using UnityEngine;
using System.Collections;

//class for Splashscreen's User Interface
public class Splashscreen_UI : MonoBehaviour {
	
	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	//variable to store text upon mouse hover of START button
	private string StartButtonTxt;

	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		StartButtonTxt = "Click or Press Enter";
	}

	//function to render User Interface
	void OnGUI ()
	{
		//store this scene for other scenes to refer to
		PlayerPrefs.SetInt("splashscreen", Application.loadedLevel);

		//draw splashscreen background texture
		UIM.CreateTexture(UIM.splashscreen, 0.0f, 0.0f, Screen.width, Screen.height, false);

		if(!AutoFade.Fading)
		{
			//if button is clicked or Enter key is pressed, fade into and go to Mainmenu scene
			if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.1f, "START", "STARTBUTTON")
			  ||
			   Input.GetKeyDown(KeyCode.Return))
			{
				AutoFade.LoadLevel("Mainmenu", 1.0f, 1.0f, Color.black);
			}
		}

		if(GUI.tooltip == "STARTBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.45f, Screen.height * 0.75f, Screen.width * 0.1f, Screen.height * 0.05f, StartButtonTxt);
		}
	}
}
