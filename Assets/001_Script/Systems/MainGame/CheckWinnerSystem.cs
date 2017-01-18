using UnityEngine;
using System.Collections;
using Entitas;
public class CheckWinnerSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Group _groupMovers;
	public void SetPool (Pool pool)
	{
		_groupMovers = pool.GetGroup (Matcher.Mover);
	}

	#endregion

	#region IReactiveExecuteSystem implementation

	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		Entity e;
		var ens = _groupMovers.GetEntities ();
		for (int i = 0; i < ens.Length; i++) {
			e = ens [i];


		}
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.Phase04_CheckGameOver.OnEntityAdded ();
		}
	}

	#endregion
}
