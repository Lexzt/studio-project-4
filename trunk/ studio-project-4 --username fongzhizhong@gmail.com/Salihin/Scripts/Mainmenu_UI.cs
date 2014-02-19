using UnityEngine;
using System.Collections;

public class Mainmenu_UI : MonoBehaviour {

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
		if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.1f, "Lobby"))
		{
			//Application.LoadLevel("Lobby");
			AutoFade.LoadLevel("Lobby", 3.0f, 1.0f, Color.black);
		}

		//if button is clicked, fade into and go to Options scene
		if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.1f, "Options"))
		{
			AutoFade.LoadLevel("Options", 3.0f, 1.0f, Color.black);
		}

		//if button is clicked, fade into and go to Credits scene
		if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.5f, Screen.width * 0.4f, Screen.height * 0.1f, "Credits"))
		{
			AutoFade.LoadLevel("Credits", 3.0f, 1.0f, Color.black);
		}

		//if button is clicked, fade into and go to Exit Game
		if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.7f, Screen.width * 0.4f, Screen.height * 0.1f, "Exit Game"))
		{
			Application.Quit();
		}
	}
}
