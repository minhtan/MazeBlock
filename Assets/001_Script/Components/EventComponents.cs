using UnityEngine;
using System.Collections;
using Entitas;

[Entitas.CodeGenerator.SingleEntity]
public class BoardSet : IComponent {
}

[Entitas.CodeGenerator.SingleEntity]
public class MouseClick : IComponent {
	public Vector3 screenPosition;
}

[Entitas.CodeGenerator.SingleEntity]
public class GameOver : IComponent {
	public Winner winner;
}