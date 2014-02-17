using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {

	//variable to store gameobject which is a prefab
	public GameObject ProjectilePrefab;

	//variables to store Player position, Item offset and Item position when equipped
	private Vector3 PlayerPos;
	public Vector3 ItemOffset;
	private Vector3 ItemPosInHand;

	// Use this for initialization
	void Start () {
		PlayerPos = transform.position;
		ItemPosInHand = PlayerPos + ItemOffset;
	}
	
	// Update is called once per frame
	void Update () {
		//instantiate only when right mouse button is clicked
		if(Input.GetMouseButtonDown(1))
		{
			Instantiate(ProjectilePrefab, ItemPosInHand, Quaternion.identity);
		}
	}
}
