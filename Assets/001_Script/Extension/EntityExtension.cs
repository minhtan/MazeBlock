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
}
