using UnityEngine;
using System.Collections;
using Entitas;

public class GameInitSystem : IInitializeSystem, ISetPool {
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
		_pool.SetGameSettings (7, 7, 1f);
		_pool.SetNodeDistance (
			_pool.gameSettings.distanceBtwNode, 
			_pool.gameSettings.distanceBtwNode / (Mathf.Sqrt (2f) / 2)
		);
		_pool.isPhase01_PlayerTurn = true;
	}
	#endregion
}
