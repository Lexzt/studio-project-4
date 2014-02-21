using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour {

	public int lengthOfRay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Shoot a ray from the crosshair?
		RaycastHit hit;
		Ray line = new Ray (camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), transform.forward);
	
		if(Physics.Raycast(line, out hit, lengthOfRay))
		{
			if(hit.collider.tag == "PlayerWeapons")
			{
				print ("Crosshair over a weapon");
				hit.collider.gameObject.renderer.material.color = Color.blue;
			}
		}
	
	
	}
}