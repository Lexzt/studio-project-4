using UnityEngine;
using System.Collections;

public class Projectile_Script : MonoBehaviour {

	//varaible for projectile's speed
	public float projectile_speed;

	//variable to store offset in order for projectile to travel towards the centre
	private Vector3 offset;

	//variables to access variables in another gameobject and script
	GameObject Player;
	Player_Script P_S;

	//variable to store the transform of the gameobject that this script is attached to
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		//store the transform of the gameobject that this script is attached to
		myTransform = transform;

		Player = GameObject.Find("Player");
		P_S = Player.GetComponent<Player_Script>();

		offset = new Vector3(-1.0f * P_S.ItemOffset.x, 0.0f, 20.0f).normalized;
	}

	// Update is called once per frame
	void Update () {

		//calculations for projectile to travel
		Vector3 direction = (Camera.main.transform.forward + offset).normalized;
		myTransform.Translate(direction * projectile_speed * Time.deltaTime);
	}
}
