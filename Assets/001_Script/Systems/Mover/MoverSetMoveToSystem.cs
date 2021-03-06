﻿using UnityEngine;
using System.Collections;
using Entitas;
public class MoverSetMoveToSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupMover;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMover = pool.GetGroup (Matcher.Mover);
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		Entity m;
		var movers = _groupMover.GetEntities ();
		for (int i = 0; i < movers.Length; i++) {
			m = movers [i];

			m.AddMoveTo (m.path.nodes.Dequeue());
		}
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.Phase03_MovingMovers.OnEntityAdded ();
		}
	}
	#endregion
}
