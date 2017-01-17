using UnityEngine;
using System.Collections;
using Entitas;

public class BoardDrawSystem : IReactiveSystem {
	#region Constructor
	GameObject _viewParent;
	public BoardDrawSystem(){
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

			if (e.hasView) {
				EntityLink.RemoveLink (e.view.go);
				Lean.LeanPool.Despawn (e.view.go);
			}

			var prefToLoad = "nodePrefab";
			if (e.node.isBlocked) {
				prefToLoad = "nodePrefabBlocked";	
			}else if (e.hasExit) {
				prefToLoad = "nodePrefabExit";
			}else if ((e.position.x + e.position.z) % 2 == 0) {
				prefToLoad = "nodePrefab";
			} else {
				prefToLoad = "nodePrefab1";
			}
			var name = "node" + e.position.x + "/" + e.position.z;

			e.AddCoroutineTask (e.CreateView(prefToLoad, name, (view) => {
				e.ReplaceView(view);
				EntityLink.AddLink(view, e);
			}, _viewParent.transform), true);
		}
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.Node.OnEntityAdded ();
		}
	}
	#endregion
}
