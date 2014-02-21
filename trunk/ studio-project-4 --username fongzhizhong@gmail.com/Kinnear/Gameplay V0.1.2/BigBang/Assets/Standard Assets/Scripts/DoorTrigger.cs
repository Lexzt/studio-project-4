using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public bool closed = true;
	public bool openClockwise = true;

	Vector3 originalRotation;
	Vector3 doorOpenRotation;

	Vector3 fixAngle;

	float angleToGoTo = 90.0f;

	void Start()
	{
		Screen.lockCursor = true;
		// The original rotation of the door
		originalRotation = transform.eulerAngles;
		fixAngle = transform.eulerAngles;

		// The final opened position of the door
		if(openClockwise)
		{
			doorOpenRotation = new Vector3 (originalRotation.x, originalRotation.y + angleToGoTo, originalRotation.z);
		}
		else{
			doorOpenRotation = new Vector3 (originalRotation.x, originalRotation.y - angleToGoTo, originalRotation.z);
		}
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.E))
		{
			if(closed)
			{
				closed = false;
			}
			else
			{
				closed = true;
			}
			//Screen.lockCursor = true;
		}

		if(!closed)
		{
			fixAngle = Vector3.Slerp(fixAngle, doorOpenRotation, Time.deltaTime);
			transform.eulerAngles = fixAngle;
		}
		else
		{
			fixAngle = Vector3.Slerp(fixAngle, originalRotation, Time.deltaTime);
			transform.eulerAngles = fixAngle;
		}
	}
}
