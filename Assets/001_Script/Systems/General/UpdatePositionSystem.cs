using UnityEngine;
using System.Collections;
using Entitas;

public class UpdatePositionSystem : IReactiveSystem, IEnsureComponents {
	#region IEnsureComponents implementation

	public IMatcher ensureComponents {
		get {
			return Matcher.View;
		}
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		Entity e;
		for (int i = 0; i < entities.Count; i++) {
			e = entities [i];

			e.view.go.transform.position = new Vector3 (e.position.x, 0.1f, e.position.z);
		}
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.Position.OnEntityAdded ();
		}
	}
	#endregion
	
}
