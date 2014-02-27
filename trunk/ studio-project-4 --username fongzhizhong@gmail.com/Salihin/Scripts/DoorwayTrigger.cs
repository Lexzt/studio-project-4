using UnityEngine;
using System.Collections;

public class DoorwayTrigger : MonoBehaviour {
	
	// Triggers the doors when a player comes nearby
	void OnTriggerStay(Collider player)
	{
		if(player.CompareTag("Player"))
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				// Broadcast message to all doors inside the collider
				BroadcastMessage("CloseOpen");
			}
		}
	}
}