using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {

	bool playerAttacking = true;
	public GameObject weapon;
	Vector3 weaponOriginalRotation;
	Vector3 weaponTopRotation, weaponBottomRotation;
	Vector3 fixAngle;
	bool reachedTop = false, reachedBottom = false;
	float animSpeed = 40.0f;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		weapon = GameObject.Find ("MainPlayer").GetComponentInChildren<ObjectPickup>().holding;

		// If we are holding a player weapon
		if(weapon != null && Input.GetMouseButtonDown(0) && playerAttacking)
		{
			//set to attacking
			playerAttacking = false;
			weaponOriginalRotation = weapon.transform.localEulerAngles;
			fixAngle = weaponOriginalRotation;
			weaponBottomRotation = weaponOriginalRotation + new Vector3(70, 0, 0);
			weaponTopRotation = weaponOriginalRotation - new Vector3(40, 0, 0);
			weapon.GetComponentInChildren<WeaponDamage>().doesDamage = true;
		}

		// if the player is attacking
		if(!playerAttacking && weapon != null)
		{
			// reached the top of the hit
			if(!reachedTop)
			{
				fixAngle = Vector3.Slerp(fixAngle, weaponTopRotation, Time.deltaTime * animSpeed);
				weapon.transform.localEulerAngles = fixAngle;
				if(fixAngle == weaponTopRotation)
				{
					reachedTop = true;
				}
			}

			// if reached top and havent went to bottom
			else if(reachedTop && !reachedBottom)
			{
				fixAngle = Vector3.Slerp(fixAngle, weaponBottomRotation, Time.deltaTime * animSpeed);
				weapon.transform.localEulerAngles = fixAngle;
				if(fixAngle == weaponBottomRotation)
				{
					reachedBottom = true;
				}
			}

			else if(reachedTop && reachedBottom)
			{
				fixAngle = Vector3.Slerp(fixAngle, weaponOriginalRotation, Time.deltaTime * animSpeed);
				weapon.transform.localEulerAngles = fixAngle;
				if(fixAngle == weaponOriginalRotation)
				{
					reachedBottom = reachedTop = false;
					playerAttacking = true;
					weapon.GetComponentInChildren<WeaponDamage>().doesDamage = false;
				}
			}
		}

	}



}
