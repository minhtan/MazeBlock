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
		if (!_pool.isPhase01_PlayerTurn) {
			return;
		}

		CheckClick ();
	}
	#endregion

	void CheckClick(){
		if (Input.GetMouseButtonUp(0)) {
			_pool.SetMouseClick (Input.mousePosition);
		}
	}
}
