using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

	//instance of the class
	public static ParticleManager ParticleInstance;

	//variables to store the various particles
	public GameObject Freeze, Healing, IncreasedDamage, Shield, SpeedBoost, BloodSplatter;

	private GameObject Particles;

	//function that initializes
	void Awake()
	{	
		if (ParticleInstance != null && ParticleInstance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			ParticleInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void CreateParticle(GameObject ParticleEffect, Vector3 Position)
	{
		//instantiate a particle at a particular position
		Particles = (GameObject)Instantiate(ParticleEffect, Position, Quaternion.identity);

		//delete when lifetime is over
		Destroy(Particles, ParticleEffect.particleSystem.startLifetime);
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			CreateParticle(BloodSplatter, Vector3.zero);
		}
	}
}
