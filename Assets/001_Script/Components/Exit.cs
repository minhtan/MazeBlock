using UnityEngine;
using System.Collections;
using Entitas;

public enum Player{
	player1,
	player2,
	AI
}

public class Exit : IComponent {
	public Player player;	
}
