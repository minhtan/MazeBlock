using UnityEngine;
using System.Collections;
using Entitas;

[Entitas.CodeGenerator.SingleEntity]
public class CurrentPlaying : IComponent {
	public Player player;
}
