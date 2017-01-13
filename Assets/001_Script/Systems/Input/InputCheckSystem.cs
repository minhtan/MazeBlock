using UnityEngine;
using System.Collections;
using Entitas;

public class InputCheckSystem : IExecuteSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
	}

	#endregion

	#region IExecuteSystem implementation
	public void Execute ()
	{
		if (Input.GetMouseButtonUp(0)) {
			_pool.CreateEntity ().AddMouseClick (Input.mousePosition);
		}
	}
	#endregion
}
