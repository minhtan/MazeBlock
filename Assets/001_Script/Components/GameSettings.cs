using UnityEngine;
using System.Collections;
using Entitas;

[Entitas.CodeGenerator.SingleEntity]
public class GameSettings : IComponent {
	public int row;
	public int column;
	public float distanceBtwNode;
}
