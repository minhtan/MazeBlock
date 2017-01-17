using UnityEngine;
using System.Collections;
using Entitas;
using Priority_Queue;
using System.Collections.Generic;
using System.Linq;

public class PathFindingSystem : IReactiveSystem, ISetPool, IInitializeSystem {
	#region ISetPool implementation
	Group _groupMover;
	Group _groupExit;
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMover = pool.GetGroup (Matcher.Mover);
		_groupExit = pool.GetGroup (Matcher.Exit);
	}

	#endregion

	#region IInitializeSystem implementation
	float D;
	float D2;
	public void Initialize ()
	{
		D = _pool.gameSettings.distanceBtwNode;
		D2 = _pool.gameSettings.distanceBtwNode / (Mathf.Sqrt (2f) / 2);
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	Queue<Entity> path;
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		var lastBlocked = entities.SingleEntity ();

		Entity m;
		var movers = _groupMover.GetEntities ();
		for (int i = 0; i < movers.Length; i++) {
			m = movers [i];

			path = FindPath (m.standOn.node, m.goal.node, D, D2);
			if (path == null) { // can not find path
				lastBlocked.RemoveLastBlocked ();
				return;
			} else {
				m.ReplacePath (path);
			}
		}

		lastBlocked.lastBlocked.node.ReplaceNode(true).RemoveLastBlocked ();
		_pool.isFindPathDone = true;
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.LastBlocked.OnEntityAdded ();
		}
	}

	#endregion

	SimplePriorityQueue<Entity> frontier = new SimplePriorityQueue<Entity> ();
	List<Entity> exploredNodes = new List<Entity> ();
	List<Entity> neighbors;
	Queue<Entity> FindPath(Entity start, Entity end, float D, float D2){
		frontier.Clear ();
		frontier.Enqueue (start, 0);
		exploredNodes.Clear ();
		exploredNodes.Add (start);

		Entity current;
		float cost;
		while (frontier.Count > 0) {
			current = frontier.Dequeue ();

			if (current == end) {
				return ReconstructPath (ref end, ref start);
			}

			neighbors = current.neighbors.neighbors;
			for (int i = 0; i < neighbors.Count; i++) {
				var next = neighbors [i];

				if (next.node.isBlocked || next.hasLastBlocked) {
					continue;
				}

				if ((next.position.x + next.position.z) - (current.position.x + current.position.z) == 1) {
					cost = D;
				} else {
					cost = D2;
				}

				var newCost = current.moveCost.cost + cost;
				if (!exploredNodes.Contains(next) || next.moveCost.cost > newCost) {
					next.moveCost.cost = newCost;
					next.cameFrom.origin = current;
					if (!exploredNodes.Contains(next)) {
						exploredNodes.Add (next);
					}

					var priority = newCost + GetHScore(next.position, end.position, D, D2) * 1.5f;
					frontier.Enqueue (next, priority); //smaller priority go first
				}
			}

		}

		return null;
	}

	float GetHScore(Position node, Position goal, float D, float D2){
		var dx = Mathf.Abs (node.x - goal.x);
		var dy = Mathf.Abs (node.z - goal.z);
		return D * (dx + dy) + (D2 - 2 * D) * Mathf.Min (dx, dy);
	}

	Queue<Entity> ReconstructPath(ref Entity goal, ref Entity start){
		Entity current = goal;
		Queue<Entity> path = new Queue<Entity> ();
		path.Enqueue (current);

		while (current != start) {
			current = current.cameFrom.origin;
			if (current != start) {
				path.Enqueue (current);
			}

			if (current == null) {
				return null;
			}
		}

		path = new Queue<Entity> (path.Reverse ());

		return path;
	}
}
