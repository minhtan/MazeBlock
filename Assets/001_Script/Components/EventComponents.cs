using UnityEngine;
using System.Collections;
using Entitas;

[Entitas.CodeGenerator.SingleEntity]
public class BoardSet : IComponent {

}

[Entitas.CodeGenerator.SingleEntity]
public class TurnEnded : IComponent {
	public Player player;
}

[Entitas.CodeGenerator.SingleEntity]
public class FindPathDone : IComponent{
	
}