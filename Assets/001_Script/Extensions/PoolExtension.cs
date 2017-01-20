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
			pool.isPhase01_PlayerTurn = false;
			pool.isPhase02_OpponentTurn = true;
		} else if (pool.isPhase02_OpponentTurn) {
			pool.isPhase02_OpponentTurn = false;
			pool.isPhase03_MovingMovers = true;
		} else if (pool.isPhase03_MovingMovers){
			pool.isPhase03_MovingMovers = false;
			pool.isPhase04_CheckGameOver = true;
		}else if (pool.isPhase04_CheckGameOver) {
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
