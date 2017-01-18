using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class PathDrawSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupMoversWithPath;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMoversWithPath = _pool.GetGroup (Matcher.Path);
	}
	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		//mover
		Entity mover;
		var movers = _groupMoversWithPath.GetEntities ();
		//path
		Queue<Entity> tempPath;
		int count;
		Entity current;

		for (int i = 0; i < movers.Length; i++) {
			mover = movers [i];

			tempPath = new Queue <Entity> ( mover.path.nodes );
			count = mover.path.nodes.Count;
			mover.pathView.line.SetVertexCount (count);
			for (int j = 0; j < count; j++) {
				current = tempPath.Dequeue ();
				mover.pathView.line.SetPosition (j, new Vector3 (current.position.x, 0.01f, current.position.z));
			}
		}
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.Phase02_OpponentTurn.OnEntityRemoved ();
		}
	}
	#endregion


}
