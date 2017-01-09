using UnityEngine;
using System.Collections;
using Entitas;

public class InitBoardSystem : IInitializeSystem, ISetPool {
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
		var col = _pool.gameSettings.column;
		var row = _pool.gameSettings.row;

		for (int r = 0; r < row; r++) {
			for (int c = 0; c < col; c++) {
				_pool.CreateEntity ().AddPosition (r, c).AddNode (false);
			}
		}
	}
	#endregion
}
