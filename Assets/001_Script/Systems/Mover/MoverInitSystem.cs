using UnityEngine;
using System.Collections;
using Entitas;

public class MoverInitSystem : IInitializeSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
	}

	#endregion

	#region IInitializeSystem implementation

	public void Initialize ()
	{
		var nodes = _pool.GetEntities (Matcher.Node);
		var moverX = Mathf.CeilToInt (_pool.gameSettings.row / 2);
		var moverZ = Mathf.CeilToInt (_pool.gameSettings.column / 2);
		for (int i = 0; i < nodes.Length; i++) {
			if (nodes[i].position.x == moverX && nodes[i].position.z == moverZ) {
				_pool.CreateEntity ()
					.AddPosition (nodes [i].position.x, nodes [i].position.z)
					.AddStandOn(nodes[i])
					.IsMover (true);
				return;
			}
		}
	}

	#endregion
}
