using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameManager : MonoBehaviour {
	public bool showDebug = true;
	Systems _systems;
	void Awake() {
		var pools = Pools.sharedInstance;
		pools.SetAllPools ();
		_systems = CreateSystems (pools);
	}

	void Start(){
		_systems.Initialize ();
	}

	void Update() {
		_systems.Execute ();
	}

	void OnDestroy(){
		_systems.TearDown ();
	}

	Systems CreateSystems(Pools pools) {
		Systems systems;
		if (showDebug) {
			systems = new DebugSystems ();
		} else {
			systems = new Systems ();
		}

		return systems
			.Add(pools.pool.CreateSystem( new InitGameSystem () ))
			.Add(pools.pool.CreateSystem( new InitBoardSystem () ))

			;
	}
}
