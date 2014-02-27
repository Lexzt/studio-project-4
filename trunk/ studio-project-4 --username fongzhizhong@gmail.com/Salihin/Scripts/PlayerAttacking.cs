using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour {

	bool playerAttacking = true;
	string weaponName;
	
	// Update is called once per frame
	void Update () {

		weaponName = GameObject.Find ("MainPlayer1 1 1(Clone)").GetComponentInChildren<ObjectPickup>().weaponName;
		
		// If we are holding a player weapon
		if(weaponName != null && playerAttacking)
		{
			Debug.Log(weaponName);
			
			// Enable the weapon on the character
			transform.FindChild(weaponName).gameObject.SetActive(true);

			// Player has pressed the left button
			if(Input.GetMouseButton(0))
			{
				Debug.Log("Attacked");
				GetComponentInChildren<Animation>().Play("Diamondsword");
				transform.FindChild(weaponName).GetComponentInChildren<WeaponDamage>().doesDamage = true;
			}

			// If the animation isnt playing the sword cant do any damage.
			if (!animation.IsPlaying("Diamondsword"))
			{
				transform.FindChild(weaponName).GetComponentInChildren<WeaponDamage>().doesDamage = false;
			}
		}
	}
}
