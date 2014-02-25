using UnityEngine;
using System.Collections;

public class WeaponDamage : MonoBehaviour {

	public bool doesDamage = false;

	void OnTriggerEnter(Collider obj)
	{
		print("player intersected enemy");
		
		// if the player is attacking
		if(doesDamage)
		{
			//detect for enemy collision here
			if( obj.gameObject.tag == "Enemy")
			{
				obj.gameObject.GetComponent<HealthDisplay>().ApplyDamage(10.0f);
				doesDamage = false;
			}
		}
		
	}
}
