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
		_pool.SetGameSettings (9, 9, 1f);
		_pool.SetCurrentPlaying (Player.Me);
	}
	#endregion
}
