using UnityEngine;
using System.Collections;
using Entitas;
public class MoverMoveSystem : IExecuteSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupMoveTo;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMoveTo = pool.GetGroup (Matcher.MoveTo);
	}

	#endregion

	#region IExecuteSystem implementation

	public void Execute ()
	{
		if (_groupMoveTo.count <= 0) {
			_pool.isDisableInput = false;
			return;
		}

		Entity m;
		var ens = _groupMoveTo.GetEntities ();
		for (int i = 0; i < ens.Length; i++) {
			m = ens [i];

			if (m.IsReachedNode (m.moveTo.node)) {
				m.standOn.node.IsBeingStoodOn (false);
				m.ReplaceStandOn (m.moveTo.node);
				m.moveTo.node.IsBeingStoodOn (true);
				m.RemoveMoveTo ();
			} else {
				var nextPos = Vector2.MoveTowards (
	             	new Vector2 (m.position.x, m.position.z), 
					new Vector2 (m.moveTo.node.position.x, m.moveTo.node.position.z),
	              	5f * Time.deltaTime);

				m.ReplacePosition (nextPos.x, nextPos.y);
			}
		}
	}
	#endregion
	
}
