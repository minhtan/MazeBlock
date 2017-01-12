using UnityEngine;
using System.Collections;
using Entitas;

public static class EntityExtension {
	public static Entity AddCoroutineTask(this Entity e, IEnumerator task, bool priority = false){
		if (!e.hasCoroutine) {
			e.AddCoroutine (task);
		} else {
			if (!e.hasCoroutineQueue) {
				e.AddCoroutineQueue (new System.Collections.Generic.Queue<IEnumerator> ());
			}

			if (priority) {
				e.coroutineQueue.Queue.Enqueue (e.coroutine.task);
				e.ReplaceCoroutine (task);
			} else {
				e.coroutineQueue.Queue.Enqueue (task);
			}
		}
		return e;
	}

	public static IEnumerator CreateView(this Entity e, string prefToLoad, string name, Transform parent = null){
		var r = Resources.LoadAsync<GameObject> (prefToLoad);
		while(!r.isDone){
			yield return null;
		}

		GameObject go = Lean.LeanPool.Spawn (r.asset as GameObject);
		go.name = name;
		go.transform.position = new Vector3 (e.position.x, 0, e.position.z);
		if (parent != null) {
			go.transform.SetParent (parent, false);
		}
	}

	public static IEnumerator CreateView(this Entity e, string prefToLoad, string name, System.Action<GameObject> callback, Transform parent = null){
		var r = Resources.LoadAsync<GameObject> (prefToLoad);
		while(!r.isDone){
			yield return null;
		}

		GameObject go = Lean.LeanPool.Spawn (r.asset as GameObject);
		go.name = name;
		go.transform.position = new Vector3 (e.position.x, 0, e.position.z);
		if (parent != null) {
			go.transform.SetParent (parent, false);
		}
		callback (go);
	}
}
