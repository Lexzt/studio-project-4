using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public int m_iSpeed = 5;
	public int m_iGravity = 5;

	private CharacterController cc;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine){
			cc.Move (new Vector3 (	Input.GetAxis ("Horizontal") * m_iSpeed * Time.deltaTime,
		     	    	      	 	- m_iGravity * Time.deltaTime,
		        	    	   		Input.GetAxis ("Vertical") * m_iSpeed * Time.deltaTime));
		}
	}
}
