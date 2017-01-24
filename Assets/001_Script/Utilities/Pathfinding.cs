using UnityEngine;
using System.Collections;
using Priority_Queue;
using Entitas;
using System.Collections.Generic;
using System.Linq;

public class Pathfinding {
	static SimplePriorityQueue<Entity> frontier = new SimplePriorityQueue<Entity> ();
	static List<Entity> exploredNodes = new List<Entity> ();
	static List<Entity> neighbors;
	public static Queue<Entity> FindPath(Entity start, Entity end, float D, float D2){
		frontier.Clear ();
		start.moveCost.cost = 0f;
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

				if (next.node.isBlocked || next.hasLastBlocked || next.isTemporaryBlocked) {
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

	static float GetHScore(Position node, Position goal, float D, float D2){
		var dx = Mathf.Abs (node.x - goal.x);
		var dy = Mathf.Abs (node.z - goal.z);
		return D * (dx + dy) + (D2 - 2 * D) * Mathf.Min (dx, dy);
	}

	static Queue<Entity> ReconstructPath(ref Entity goal, ref Entity start){
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
