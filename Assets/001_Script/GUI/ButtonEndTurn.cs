using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonEndTurn : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<Button> ().onClick.AddListener( () => {
			Messenger.Broadcast(Events.Game.TURN_ENDED);
		});
	}

	void OnDestroy(){
		GetComponent<Button> ().onClick.RemoveAllListeners ();
	}
}
