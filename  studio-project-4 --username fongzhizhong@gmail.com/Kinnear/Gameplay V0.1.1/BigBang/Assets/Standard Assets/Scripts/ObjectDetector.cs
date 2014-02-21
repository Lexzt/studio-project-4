using UnityEngine;
using System.Collections;

public class ObjectDetector : MonoBehaviour {

	GameObject holding = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider obj)
	{

		if(Input.GetKeyDown(KeyCode.E))
		{
			if(holding == null)
			{
				if( obj.gameObject.tag == "PlayerWeapons")
				{
						//Debug.Log(obj.gameObject.name);
						obj.transform.parent = Camera.main.transform;
						obj.transform.localPosition = new Vector3 (1.0f, -0.2f, 2.0f);
						obj.gameObject.rigidbody.isKinematic = true;
						holding = obj.gameObject;
				}
			}
			else
			{
				holding.gameObject.rigidbody.isKinematic = false;
				holding.transform.parent = null;
				holding = null;
			}
		}
	}
}
