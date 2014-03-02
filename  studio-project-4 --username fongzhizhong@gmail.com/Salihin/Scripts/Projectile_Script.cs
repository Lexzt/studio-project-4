using UnityEngine;
using System.Collections;

public class Projectile_Script : MonoBehaviour {
	
	//varaible for projectile's speed
	public float projectile_speed;
	
	//variable to store offset in order for projectile to travel towards the centre
	private Vector3 offset;
	
	//variable for projectile's gravity
	public Vector3 gravity;
	
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		//calculations for projectile to travel
		Vector3 direction = (Camera.main.transform.forward).normalized;
		rigidbody.AddForce(gravity);
		transform.Translate(direction * projectile_speed * Time.deltaTime);

	}
}
