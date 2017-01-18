using UnityEngine;
using System.Collections;
using Entitas;
using Priority_Queue;
using System.Collections.Generic;
using System.Linq;

public class CheckLastBlockedValiditySystem : IReactiveSystem, ISetPool, IInitializeSystem {
	#region ISetPool implementation
	Group _groupMover;
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMover = pool.GetGroup (Matcher.Mover);
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

			path = Pathfinding.FindPath (m.standOn.node, m.goal.node, D, D2);
			if (path == null) { // can not find path
				lastBlocked.RemoveLastBlocked ().IsInvalid(true);
				if (_pool.isPhase02_OpponentTurn) {
					_pool.isPhase02_OpponentTurn = true;
				}
				return;
			}
		}

		lastBlocked.lastBlocked.node.ReplaceNode(true).RemoveLastBlocked ();
		_pool.NextPhase ();
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.LastBlocked.OnEntityAdded ();
		}
	}

	#endregion
}
