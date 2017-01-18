using UnityEngine;
using System.Collections;
using Entitas;
public class BoardSetBlockSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		var mouseClick = entities.SingleEntity ();

		var ray = Camera.main.ScreenPointToRay (mouseClick.mouseClick.screenPosition);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo)) {
			var e = EntityLink.GetEntity (hitInfo.collider.gameObject);
			if (e != null && !e.node.isBlocked && !e.isBeingStoodOn && !e.isInvalid) {
				e.AddLastBlocked (e);
			}
		}

		_pool.DestroyEntity (mouseClick);
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.MouseClick.OnEntityAdded ();
		}
	}
	#endregion
}
