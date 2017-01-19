using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class AISetBlockSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupMovers;
	Group _groupNodes;
	Group _groupEvaluatedNodes;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMovers = pool.GetGroup (Matcher.Mover);
		_groupNodes = pool.GetGroup (Matcher.AllOf(Matcher.Node).NoneOf(Matcher.Unblockable, Matcher.BeingStoodOn));
		_groupEvaluatedNodes = pool.GetGroup (Matcher.AllOf(Matcher.TotalMoversCost).NoneOf(Matcher.Unblockable));
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	Queue<Entity> path;
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		float playerCost = 0;
		float AICost = 0;
		bool invalidNode;
		Entity n;
		Entity m;
		var movers = _groupMovers.GetEntities ();
		var nodes = _groupNodes.GetEntities ();

		for (int i = 0; i < nodes.Length; i++) {
			n = nodes [i];

			invalidNode = false;
			n.IsTemporaryBlocked (true);
			
			for (int j = 0; j < movers.Length; j++) {
				m = movers[j];
				playerCost = 0f;
				AICost = 0f;

				path = Pathfinding.FindPath (m.standOn.node, m.goal.node, _pool.nodeDistance.D, _pool.nodeDistance.D2);
				if (path == null) {
					n.IsUnblockable (true);
					invalidNode = true;
					break;
				} else {
					if (m.mover.player == Player.Me) {
						playerCost += GetMoveCost (path);
					}else if (m.mover.player == Player.Opponent) {
						AICost += GetMoveCost (path);
					}
				}
			}

			n.IsTemporaryBlocked (false);
			if (invalidNode) {
				continue;
			}else{
				n.ReplaceTotalMoversCost (playerCost, AICost);
			}
		}

		int choosenOne = 0;
		Entity en;
		var ens = _groupEvaluatedNodes.GetEntities ();
		for (int i = 0; i < ens.Length; i++) {
			en = ens [i];

			if (en.totalMoversCost.AICost < ens[choosenOne].totalMoversCost.AICost && 
				en.totalMoversCost.AICost < en.totalMoversCost.playerCost )
			{
				choosenOne = i;	
			}
		}

		ens [choosenOne].ReplaceNode (true).IsUnblockable (true);
		for (int i = 0; i < movers.Length; i++) {
			m = movers [i];

			path = Pathfinding.FindPath (m.standOn.node, m.goal.node, _pool.nodeDistance.D, _pool.nodeDistance.D2);
			m.ReplacePath (path);
		}
		_pool.NextPhase ();
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.Phase02_OpponentTurn.OnEntityAdded ();
		}
	}

	#endregion

	float GetMoveCost(Queue<Entity> path){
		float cost = 0;
		foreach (var item in path) {
			cost += item.moveCost.cost;
		}
		return cost;
	}
}
