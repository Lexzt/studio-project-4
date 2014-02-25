using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

	public enum PowerupTypes 
	{
		Speed, DamageIncrease
	}

	public PowerupTypes powerupType;

	float powerupTimeStamp;

	float cooldown = 3.0f;

	// Use this for initialization
	void Start () {
		powerupTimeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		// Update powerup
		// PowerUps are swappable

	
	}

	public PowerupTypes PickedUpPowerUp()
	{
		switch(powerupType)
		{
		case PowerupTypes.Speed:
			Debug.Log("Obtained Speed");
			break;
		case PowerupTypes.DamageIncrease:
			Debug.Log("Obtained Damage Increase");
			break;
		}

		//return the type of powerup
		return powerupType;
	}

	public bool CooldownOver()
	{
		Debug.Log (Time.time);
		if(powerupTimeStamp < Time.time)
		{
			return true;
		}
		else{
			return false;
		}

	}

	public bool UsePowerUp()
	{
		// if the duration has finished then we can use the powerup
		if(powerupTimeStamp <= Time.time )
		{
			powerupTimeStamp = Time.time + cooldown;
			return true;
		}
		else
		{
			return false;
		}
	}
}
