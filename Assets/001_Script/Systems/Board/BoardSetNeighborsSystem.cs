using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class BoardSetNeighborsSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupNodes;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupNodes = _pool.GetGroup (Matcher.Node);
	}

	#endregion
	
	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		var boardSet = entities.SingleEntity ();
	
		Entity node;
		var nodes = _groupNodes.GetEntities ();
		List<Entity> neighbors;
		for (int i = 0; i < nodes.Length; i++) {
			node = nodes [i];

			FindNeighbors (node, nodes, out neighbors);
			node.AddNeighbors (neighbors);
		}

		_pool.DestroyEntity (boardSet);
	}
	#endregion

	#region IReactiveSystem implementation
	public TriggerOnEvent trigger {
		get {
			return Matcher.BoardSet.OnEntityAdded ();
		}
	}
	#endregion

	void FindNeighbors(Entity current, Entity[] nodes, out List<Entity> neighbors){
		neighbors = new List<Entity> ();
		for (int i = 0; i < nodes.Length; i++) {
			if ( Mathf.Abs(current.position.x - nodes[i].position.x) <= 1 && Mathf.Abs(current.position.z - nodes[i].position.z) <= 1 && current != nodes[i]) {
				neighbors.Add (nodes [i]);
				if (neighbors.Count > 7) {return;}
			}
		}
	}
}
