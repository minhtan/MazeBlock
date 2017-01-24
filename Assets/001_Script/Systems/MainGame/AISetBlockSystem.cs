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
	Group _groupUnblockable;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMovers = pool.GetGroup (Matcher.Mover);
		_groupUnblockable = pool.GetGroup (Matcher.Unblockable);
		_groupNodes = pool.GetGroup (Matcher.AllOf(Matcher.Node).NoneOf(Matcher.Blocked, Matcher.Unblockable, Matcher.BeingStoodOn));
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

		//Evaluate nodes
		for (int i = 0; i < nodes.Length; i++) {
			n = nodes [i];

			playerCost = 0f;
			AICost = 0f;
			invalidNode = false;
			n.IsTemporaryBlocked (true);
			
			for (int j = 0; j < movers.Length; j++) {
				m = movers[j];

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
				Debug.Log (n.position.x + "/" + n.position.z + " player: " + playerCost + " AI " + AICost);
				n.ReplaceTotalMoversCost (playerCost, AICost);
			}
		}

		//Get the optimal node
		int choosenOne = -1;
		Entity en;
		var ens = _groupEvaluatedNodes.GetEntities ();
		for (int i = 0; i < ens.Length; i++) {
			en = ens [i];

			if (en.totalMoversCost.AICost < en.totalMoversCost.playerCost )
			{
				if (choosenOne == -1) {
					choosenOne = i;	
				} else if(en.totalMoversCost.AICost < ens[choosenOne].totalMoversCost.AICost){
					choosenOne = i;
				}
			}
		}
		for (int i = 0; i < ens.Length; i++) {
			ens[i].RemoveTotalMoversCost ();
		}

		//Place block on choosen node
		if (choosenOne != -1) {
			Debug.Log ("chossen: " + ens[choosenOne].position.x + "/" + ens[choosenOne].position.z);
			ens [choosenOne].ReplaceNode (true).IsBlocked(true);
		}

		//Find path
		for (int i = 0; i < movers.Length; i++) {
			m = movers [i];
			
			path = Pathfinding.FindPath (m.standOn.node, m.goal.node, _pool.nodeDistance.D, _pool.nodeDistance.D2);
			m.ReplacePath (path);
			DebugPath (path);
		}

		//Reset unblockables
		var uns = _groupUnblockable.GetEntities();
		for (int i = 0; i < uns.Length; i++) {
			uns [i].IsUnblockable(false);
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

	void DebugPath(Queue<Entity> path){
		string log = "";
		foreach (var item in path) {
			log += item.position.x + "/" + item.position.z + ":" + item.moveCost.cost + "__";
		}
		Debug.Log (log);
	}
}
