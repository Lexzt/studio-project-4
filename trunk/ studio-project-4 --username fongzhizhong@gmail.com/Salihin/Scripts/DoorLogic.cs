using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour {

//	float clampMax;
//	float clampMin;
//	float damp;
//	static private float currentMouseY;
//	static private float oldMouseY = 0;
//	private float mouseVelocity;
//	private float angle;

	public float RotationSpeed = 5000000;


	void Update()
	{
		//transform.Rotate(0, (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime), 0, Space.World);
	}


	void OnMouseDrag()
	{
		if(Input.GetMouseButton(0))
		{
			transform.parent.gameObject.transform.Rotate(0, (Input.GetAxis("Mouse Y") * RotationSpeed), 0, Space.World);
			Screen.lockCursor = true;
		}

	}

//	void OnMouseDrag ()
//	{	
//		if(Input.GetMouseButton(0))
//		{
//			//track the Y mouse position
//			currentMouseY = Input.mousePosition.y;
//			//return the difference in Y positions
//			mouseVelocity = currentMouseY - oldMouseY;
//
//			//set the target rotation to the door's current rotation + the change in mouse Y value
//			angle = mouseVelocity;
//			//clamp the value to set how far the door can open in either direction
//			//angle = Mathf.Clamp(angle, clampMin, clampMax);
//			//set the euler rotation, and rotate the door
//			//var rotation = Quaternion.Euler(0, angle, 0);
//			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, damp * Time.deltaTime);   
//
//
//
//			float smooth = 100.0f;
//			Quaternion rotation =  Quaternion.Euler(0, angle + transform.parent.gameObject.transform.rotation.y, 0);
//			//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	//			transform.parent.gameObject.transform.rotation.transform.rotation = Quaternion.Slerp(transform.parent.gameObject.transform.rotation, rotation, Time.deltaTime * smooth);
//			print(angle);
//			//track the old mouse Y position
//			oldMouseY = currentMouseY;
//		}
//	}

}