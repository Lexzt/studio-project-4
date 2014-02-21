using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour {

	public int lengthOfRay;
	public GameObject holding = null;

	Quaternion instance = Quaternion.identity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Shoot a ray from the crosshair?
		RaycastHit hit;
		Ray line = new Ray (camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), transform.forward);
	
		// Debug line for the raycast
		Debug.DrawRay (camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane)), transform.forward * lengthOfRay);

		if(Physics.Raycast(line, out hit, lengthOfRay))
		{
			if(hit.collider.tag == "PlayerWeapons")
			{
				hit.collider.gameObject.renderer.material.color = Color.blue;

				// Pick up the object
				if(Input.GetKeyDown(KeyCode.E))
				{
					hit.transform.parent.parent = camera.transform;
					hit.transform.parent.localPosition = new Vector3 (1.0f, -0.2f, 2.0f);



					instance.eulerAngles = new Vector3(-10, -20, 0);
					hit.transform.parent.localEulerAngles = instance.eulerAngles;
					holding = hit.transform.parent.gameObject;
				}
			}
		}



	}
}