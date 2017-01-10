using UnityEngine;
using System.Collections;
using Entitas;

public class InitGameSystem : IInitializeSystem, ISetPool {
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
		_pool.CreateEntity ().AddGameSettings(5, 5, 1f);
	}
	#endregion
}
