using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {

	//instance of the class
	public static ProjectileManager ProjectileInstance;

	public GameObject projectile;

	private GameObject tempproj;

	//function that initializes
	void Awake()
	{	
		if (ProjectileInstance != null && ProjectileInstance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			ProjectileInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//function for player to fire projectile	
	public void InitProjectile(GameObject tempprojectile, Vector3 position)
	{
		tempproj = (GameObject)Instantiate(tempprojectile, position, Quaternion.identity);
	}

	public void DestroyProjectile(float delay)
	{
		DestroyObject(tempproj, delay);
	}
}
