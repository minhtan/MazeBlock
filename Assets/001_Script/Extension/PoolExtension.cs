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

	public static void NextPhase(this Pool pool){
		if (pool.isPhase01_PlayerTurn) {
			Debug.Log ("to phase 2");
			pool.isPhase01_PlayerTurn = false;
			pool.isPhase02_OpponentTurn = true;
		} else if (pool.isPhase02_OpponentTurn) {
			Debug.Log ("to phase 3");
			pool.isPhase02_OpponentTurn = false;
			pool.isPhase03_MovingMovers = true;
		} else if (pool.isPhase03_MovingMovers){
			Debug.Log ("to phase 4");
			pool.isPhase03_MovingMovers = false;
			pool.isPhase04_CheckGameOver = true;
		}else if (pool.isPhase04_CheckGameOver) {
			Debug.Log ("to phase 1");
			pool.isPhase04_CheckGameOver = false;
			pool.isPhase01_PlayerTurn = true;
		}
	}

	public static void RetriggerPhase(this Pool pool){
		if (pool.isPhase02_OpponentTurn) {
			pool.isPhase02_OpponentTurn = true;
		}
	}
}
