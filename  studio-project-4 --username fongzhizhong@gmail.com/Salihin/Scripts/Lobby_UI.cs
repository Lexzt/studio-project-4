using UnityEngine;
using System.Collections;

public class Lobby_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;
	
	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("Splashscreen_UI");
		UIM = SplashUI.GetComponent<UIManager>();
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//if button is clicked, fade into and go to Lobby scene
		if(UIM.CreateButton(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.1f, "Start Server"))
		{
			//AutoFade.LoadLevel("Lobby", 3.0f, 1.0f, Color.black);
			Debug.Log("Starting server");
		}
	}
}
