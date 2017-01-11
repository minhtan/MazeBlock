using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class Path : IComponent {
	public Queue<Entity> nodes;
}
