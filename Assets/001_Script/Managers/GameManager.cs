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
				//Background systems
				.Add(pools.pool.CreateSystem( new CoroutineSystem () ))
				.Add(pools.pool.CreateSystem( new CoroutineQueueSystem () ))
				//Input systems
				.Add(pools.pool.CreateSystem( new InputCheckSystem () ))
				.Add(pools.pool.CreateSystem( new InputUIListenerSystem () ))
				//Set up systems
				.Add(pools.pool.CreateSystem( new InitGameSystem () ))
				//Board systems
				.Add(pools.pool.CreateSystem( new BoardInitSystem () ))
				.Add(pools.pool.CreateSystem( new BoardSetNeighborsSystem () ))
				.Add(pools.pool.CreateSystem( new BoardDrawSystem () ))
				.Add(pools.pool.CreateSystem( new BoardSetBlockSystem () ))
				//Mover systems
				.Add(pools.pool.CreateSystem( new MoverInitSystem () ))
				.Add(pools.pool.CreateSystem( new MoverDrawSystem () ))
				//Path systems
				.Add(pools.pool.CreateSystem( new PathFindingSystem () ))
				.Add(pools.pool.CreateSystem( new PathCreateViewSystem () ))
				.Add(pools.pool.CreateSystem( new PathDrawSystem () ))
			;
	}
}
