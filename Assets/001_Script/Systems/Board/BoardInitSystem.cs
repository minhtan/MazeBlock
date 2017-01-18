using UnityEngine;
using System.Collections;
using Entitas;

public class BoardInitSystem : IInitializeSystem, ISetPool {
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
		var dist = _pool.gameSettings.distanceBtwNode;
		Entity node;

		for (int r = 0; r < row; r++) {
			for (int c = 0; c < col; c++) {
				node = _pool.CreateEntity ();

				node.AddPosition (r * dist, c * dist).AddMoveCost(0f).AddCameFrom(null).AddNode (false);

				if (r==0 && c==col-1) {
					node.AddExit (Player.Me);
				}else if (r==row-1 && c==col-1){
					node.AddExit (Player.Opponent);	
				}
			}
		}

		_pool.isBoardSet = true;
	}
	#endregion
}
