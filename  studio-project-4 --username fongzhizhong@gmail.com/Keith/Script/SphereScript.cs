using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {

	void OnTriggerEnter() {
		Debug.Log ("In Trigger");
		Vector3 newCol = new Vector3 (1, 0, 0);
		networkView.RPC("SetColor",RPCMode.AllBuffered, newCol);
	}

	void OnTriggerExit() {
		Debug.Log ("Out Trigger");
		Vector3 newCol = new Vector3 (1, 1, 1);
		networkView.RPC("SetColor",RPCMode.AllBuffered, newCol);
	}

	[RPC]
	void SetColor(Vector3 newColor){
		renderer.material.color = new Color (newColor.x, newColor.y, newColor.z, 1);
	}
}
