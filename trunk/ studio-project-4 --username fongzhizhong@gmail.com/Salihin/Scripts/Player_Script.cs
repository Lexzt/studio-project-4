using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {
	
	//variable to store gameobject which is a prefab
	public GameObject ProjectilePrefab;

	//variables to store Player position, Item offset and Item position when equipped
	public Vector3 ItemOffset;
	private Vector3 ItemPosInHand;

	//variables to access variables in another gameobject and script
	GameObject ProjManager;
	ProjectileManager PM;

	// Use this for initialization
	void Start () {
		//ItemPosInHand = transform.position + ItemOffset;

		//ProjectilePrefab.transform.position = ItemPosInHand;

		PM = GetComponent<ProjectileManager>();
	}
		
	// Update is called once per frame
	void Update () {
		//instantiate only when right mouse button is clicked
		if(Input.GetMouseButtonDown(1))
		{
			PM.InitProjectile(PM.projectile, Vector3.zero);
			PM.DestroyProjectile(2);
		}
	}
}
