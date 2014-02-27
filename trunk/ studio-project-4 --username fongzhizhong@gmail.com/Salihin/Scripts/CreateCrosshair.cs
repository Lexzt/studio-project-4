using UnityEngine;
using System.Collections;

public class CreateCrosshair : MonoBehaviour {

	Rect position;
	public Texture2D crosshairTexture;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		position = new  Rect ((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) /2,
		                      crosshairTexture.width, crosshairTexture.height);
	}

	void OnGUI()
	{
		GUI.DrawTexture(position, crosshairTexture);
	}
}