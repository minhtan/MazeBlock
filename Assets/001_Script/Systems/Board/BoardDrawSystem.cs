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
		Entity e;
		for (int i = 0; i < entities.Count; i++) {
			e = entities [i];

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

			e.AddCoroutineTask (e.CreateView(prefToLoad, name, _viewParent.transform), true);
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
