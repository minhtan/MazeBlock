using UnityEngine;
using System.Collections;
using Entitas;

public enum Player{
	Me,
	Opponent
}

public enum Winner{
	Me,
	Opponent,
	tied
}

public class Exit : IComponent {
	public Player player;	
}
