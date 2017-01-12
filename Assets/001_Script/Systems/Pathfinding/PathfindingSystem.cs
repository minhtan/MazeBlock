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
		D2 = _pool.gameSettings.distanceBtwNode * (Mathf.Sqrt (2f) / 2);
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	Queue<Entity> path;
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		var turnEnded = entities.SingleEntity ();

		Entity m;
		Entity e;
		float totalCost;
		var movers = _groupMover.GetEntities ();
		var exits = _groupExit.GetEntities ();
		for (int i = 0; i < movers.Length; i++) {
			m = movers [i];

			for (int j = 0; j < exits.Length; j++) {
				e = exits [j];

				path = FindPath (m.standOn.node, e, D, D2, out totalCost);
				if (j == 0) {
					m.ReplacePath (path);
					m.ReplaceMoveCost (totalCost);
				} else if (m.moveCost.cost > totalCost) {
					m.ReplacePath (path);
				}
			}
		}

		_pool.CreateEntity ().IsFindPathDone (true);
		_pool.DestroyEntity (turnEnded);
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.TurnEnded.OnEntityAdded ();
		}
	}

	#endregion

	SimplePriorityQueue<Entity> frontier = new SimplePriorityQueue<Entity> ();
	List<Entity> exploredNodes = new List<Entity> ();
	List<Entity> neighbors;
	Queue<Entity> FindPath(Entity start, Entity end, float D, float D2, out float totalCost){
		frontier.Clear ();
		frontier.Enqueue (start, 0);
		exploredNodes.Clear ();
		exploredNodes.Add (start);

		Entity current;
		float cost;
		while (frontier.Count > 0) {
			current = frontier.Dequeue ();

			if (current == end) {
				return ReconstructPath (ref end, ref start, out totalCost);
			}

			neighbors = current.neighbors.neighbors;
			for (int i = 0; i < neighbors.Count; i++) {
				var next = neighbors [i];

				if (next.node.isBlocked) {
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

		totalCost = 0;
		return null;
	}

	float GetHScore(Position node, Position goal, float D, float D2){
		var dx = Mathf.Abs (node.x - goal.x);
		var dy = Mathf.Abs (node.z - goal.z);
		return D * (dx + dy) + (D2 - 2 * D) * Mathf.Min (dx, dy);
	}

	Queue<Entity> ReconstructPath(ref Entity goal, ref Entity start, out float totalCost){
		totalCost = 0;
		Entity current = goal;
		Queue<Entity> path = new Queue<Entity> ();
		path.Enqueue (current);
		totalCost += current.moveCost.cost;

		while (current != start) {
			current = current.cameFrom.origin;
			path.Enqueue (current);
			totalCost += current.moveCost.cost;

			if (current == null) {
				return null;
			}
		}

		path = new Queue<Entity> (path.Reverse ());

		return path;
	}
}
