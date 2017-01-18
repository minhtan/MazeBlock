using UnityEngine;
using System.Collections;
using Entitas;
public class InputUIListenerSystem : IInitializeSystem, ITearDownSystem, ISetPool{
	#region ISetPool implementation
	Pool _pool;
	public void SetPool (Pool pool)
	{
		_pool = pool;
	}

	#endregion

	#region IInitializeSystem implementation
	public void Initialize ()
	{
		Messenger.AddListener (Events.Game.TURN_ENDED, TurnEnded);
	}
	#endregion

	#region ITearDownSystem implementation
	public void TearDown ()
	{
		Messenger.RemoveListener (Events.Game.TURN_ENDED, TurnEnded);
	}
	#endregion

	void TurnEnded(){
		_pool.NextPhase ();
	}
}
