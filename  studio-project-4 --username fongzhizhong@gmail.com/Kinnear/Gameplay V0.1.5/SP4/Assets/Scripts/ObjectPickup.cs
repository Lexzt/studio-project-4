﻿using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour {

	public int lengthOfRay;
	public GameObject holding = null;

	// type of powerup
	public GameObject powerup = null;
	Vector3 weaponObtainedAngle = new Vector3(90, 60, 0);
	Vector3 weaponObtainedPosition = new Vector3 (1.0f, -1f, 2.0f);

	Quaternion instance = Quaternion.identity;

	// Update is called once per frame
	void Update () {
		Screen.lockCursor = true;
		
		// Shoot a ray from the crosshair?
		RaycastHit hit;
		Ray line = new Ray (camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), transform.forward);
	
		// Debug line for the raycast
		Debug.DrawRay (camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), transform.forward * lengthOfRay);

		if(Physics.Raycast(line, out hit, lengthOfRay))
		{
			if(hit.collider.tag == "PlayerWeapon")
			{
				hit.collider.gameObject.renderer.material.color = Color.blue;

				// Pick up the object
				if(Input.GetKeyDown(KeyCode.E))
				{
					hit.transform.parent.parent = camera.transform;
					hit.transform.parent.localPosition = weaponObtainedPosition;

					instance.eulerAngles = weaponObtainedAngle;
					hit.transform.parent.localEulerAngles = instance.eulerAngles;
					holding = hit.transform.parent.gameObject;
				}
			}

			else if(hit.collider.tag == "PowerUp")
			{
				hit.collider.gameObject.renderer.material.color = Color.blue;
				
				// Pick up the object
				if(Input.GetKeyDown(KeyCode.E))
				{
					powerup = hit.transform.gameObject;
				}
			}
		}


		// Use the powerup
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(powerup != null)
			{
				// the cooldown has finished so we can use a powerup
				if(powerup.GetComponent<Powerups>().UsePowerUp())
				{
					if(powerup.GetComponent<Powerups>().powerupType == Powerups.PowerupTypes.Speed)
					{
						GameObject.Find("MainPlayer").GetComponent<CharacterMotorC>().movement.maxForwardSpeed = 30;
					}
					else if (powerup.GetComponent<Powerups>().powerupType == Powerups.PowerupTypes.DamageIncrease)
					{

					}
				}

			}
		}

		// Check if the powerup timer has finished. if yes we remove the temporary ability
		if(powerup != null)
		{
			// the cooldown has finished so we can use a powerup
			if(powerup.GetComponent<Powerups>().CooldownOver())
			{
				if(powerup.GetComponent<Powerups>().powerupType == Powerups.PowerupTypes.Speed)
				{
					GameObject.Find("MainPlayer").GetComponent<CharacterMotorC>().movement.maxForwardSpeed = 10;
				}
				else if (powerup.GetComponent<Powerups>().powerupType == Powerups.PowerupTypes.DamageIncrease)
				{
					
				}
			}
			
		}



	}
}