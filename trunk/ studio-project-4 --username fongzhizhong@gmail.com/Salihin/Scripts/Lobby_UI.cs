using UnityEngine;
using System.Collections;

public class Lobby_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;
	
	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("Splashscreen_UI");
		UIM = SplashUI.GetComponent<UIManager>();

		BackButtonTxt = "Click or Press ESC";
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

		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(UIM.CreateButton(Screen.width * 0.01f, Screen.height * 0.88f, Screen.width * 0.05f, Screen.height * 0.1f, "BACK", "BACKBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.Escape))
		{
			int mainmenu = PlayerPrefs.GetInt("mainmenu");
			AutoFade.LoadLevel(mainmenu, 1.0f, 1.0f, Color.black);
		}

		if(GUI.tooltip == "BACKBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.82f, Screen.width * 0.1f, Screen.height * 0.05f, BackButtonTxt);
		}
	}
}
