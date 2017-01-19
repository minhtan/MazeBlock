using UnityEngine;
using System.Collections;
using Entitas;
public class CheckGameOverSystem : IReactiveSystem, ISetPool {
	#region ISetPool implementation
	Pool _pool;
	Group _groupMovers;
	public void SetPool (Pool pool)
	{
		_pool = pool;
		_groupMovers = pool.GetGroup (Matcher.Mover);
	}

	#endregion

	#region IReactiveExecuteSystem implementation
	int meScore;
	int opponentScore;
	public void Execute (System.Collections.Generic.List<Entity> entities)
	{
		meScore = 0;
		opponentScore = 0;
		Entity e;
		var gameEnd = false;
		var ens = _groupMovers.GetEntities ();
		for (int i = 0; i < ens.Length; i++) {
			e = ens [i];

			if (e.IsReachedNode (e.goal.node)) {
				if (e.mover.player == Player.Me) {
					meScore += 1;
				}else if (e.mover.player == Player.Opponent) {
					opponentScore += 1;
				}
				gameEnd = true;
			}
		}

		if (gameEnd) {
			if (meScore > opponentScore) {
				_pool.SetGameOver (Player.Me);
			} else if (meScore < opponentScore) {
				_pool.SetGameOver (Player.Opponent);
			} else {
				_pool.SetGameOver (Player.None);
			}
		} else {
			_pool.NextPhase ();
		}
	}

	#endregion

	#region IReactiveSystem implementation

	public TriggerOnEvent trigger {
		get {
			return Matcher.Phase04_CheckGameOver.OnEntityAdded ();
		}
	}

	#endregion
}
