using UnityEngine;
using System.Collections;
using Entitas;

public class PathCreateViewSystem : IReactiveSystem {
	#region Constructor
	GameObject _viewParent;
	public PathCreateViewSystem(){
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
			var mover = entities [i];

			mover.AddCoroutineTask(mover.CreateView("pathLine", "line" + i, (go) => {
				mover.AddPathView(go.GetComponent<LineRenderer>());
			}, _viewParent.transform));
		}
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.Mover.OnEntityAdded ();
		}
	}

	#endregion
}
