using UnityEngine;
using System.Collections;
using Entitas;
public enum ViewOrder{
	under = -1,
	middle = 0,
	top = 1
}

public class View : IComponent {
	public GameObject go;
}
