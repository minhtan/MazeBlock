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
		for (int i = 0; i < entities.Count; i++) {
			var e = entities [i];

			var prefToLoad = "moverPrefabPlayer";
			if (e.mover.player == Player.Me) {
				prefToLoad = "moverPrefabPlayer";
			} else {
				prefToLoad = "moverPrefabAI";
			}
			var name = "mover";

			e.AddCoroutineTask (e.CreateView(prefToLoad, name, (go) => {
				e.AddView(go);
			}, _viewParent.transform, ViewOrder.top), true);
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
