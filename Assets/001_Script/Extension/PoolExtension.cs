using UnityEngine;
using System.Collections;
using Entitas;

public static class PoolExtension {
	public static Entity FindExitNode(this Pool pool, Player player){
		var exits = pool.GetEntities (Matcher.Exit);
		for (int i = 0; i < exits.Length; i++) {
			var e = exits [i];

			if (e.exit.player == player) {
				return e;
			}
		}
		return null;
	}
}
