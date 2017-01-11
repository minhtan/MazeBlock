using UnityEngine;
using System.Collections;
using Entitas;

public class MoverDrawSystem : IReactiveSystem {
	#region Constructor
	GameObject _viewParent;
	public MoverDrawSystem(){
		_viewParent = GameObject.Find ("View");
		if(_viewParent == null){
			_viewParent = new GameObject ("View");
			_viewParent.transform.position = Vector3.zero;
		}
	}
	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		Entity e;
		for (int i = 0; i < entities.Count; i++) {
			e = entities [i];

			var prefToLoad = "moverPrefab";
			var name = "mover";

			e.AddCoroutineTask (e.CreateView(prefToLoad, name, _viewParent.transform), true);
		}
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.Mover.OnEntityAdded();
		}
	}
	#endregion
	
}
