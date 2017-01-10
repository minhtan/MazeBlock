using UnityEngine;
using System.Collections;
using Entitas;

public class DrawBoardSystem : IReactiveSystem {
	#region Constructor
	GameObject _boardViewParent;
	public DrawBoardSystem(){
		_boardViewParent = GameObject.Find ("BoardView");
		if(_boardViewParent == null){
			_boardViewParent = new GameObject ("BoardView");
			_boardViewParent.transform.position = Vector3.zero;
		}
	}
	#endregion

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		for (int i = 0; i < entities.Count; i++) {
			var e = entities [i];

			e.AddCoroutineTask(CreateView(e), true);
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

	IEnumerator CreateView(Entity e){
		var prefToLoad = "nodePrefab";
		if (e.hasExit) {
			prefToLoad = "nodePrefabExit";
		}else if ((e.position.x + e.position.z) % 2 == 0) {
			prefToLoad = "nodePrefab";
		} else {
			prefToLoad = "nodePrefab1";
		}
		var r = Resources.LoadAsync<GameObject> (prefToLoad);
		while(!r.isDone){
			yield return null;
		}

		GameObject go = Lean.LeanPool.Spawn (r.asset as GameObject);
		go.name = e.position.x + "/" + e.position.z;
		go.transform.position = new Vector3 (e.position.x, 0, e.position.z);
		go.transform.SetParent (_boardViewParent.transform, false);
	}
}
