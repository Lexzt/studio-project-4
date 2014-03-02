using UnityEngine;
using System.Collections;

public class Mainmenu_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;

	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		SplashSound = GameObject.Find("Sound");
		SM = SplashSound.GetComponent<SoundManager>();

		BackButtonTxt = "Click or Press ESC";
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//store this scene for other scenes to refer to
		PlayerPrefs.SetInt("mainmenu", Application.loadedLevel);

//		//if button is clicked, fade into and go to Lobby scene
//		if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.1f, "LOBBY"))
//		{
//			AutoFade.LoadLevel("Lobby", 1.0f, 1.0f, Color.black);
//		}

		//draw mainmenu background texture
		UIM.CreateTexture(UIM.mainmenu_background, 0.0f, 0.0f, Screen.width, Screen.height, false);

		//if button is clicked, fade into and go to Lobby scene
		if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.play))
		{
			SM.MakeSound(SM.button_click, transform.position);
			AutoFade.LoadLevel("Gameplay", 0.5f, 0.5f, Color.black);
		}

		//if button is clicked, fade into and go to Options scene
		if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.35f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.options))
		{
			SM.MakeSound(SM.button_click, transform.position);
			AutoFade.LoadLevel("Options", 0.5f, 0.5f, Color.black);
		}

		//if button is clicked, fade into and go to Options scene
		if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.about))
		{
			SM.MakeSound(SM.button_click, transform.position);
			AutoFade.LoadLevel("About", 0.5f, 0.5f, Color.black);
		}

		//if button is clicked, fade into and go to Credits scene
		if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.55f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.credits))
		{
			SM.MakeSound(SM.button_click, transform.position);
			AutoFade.LoadLevel("Credits", 0.5f, 0.5f, Color.black);
		}

		//if button is clicked, fade into and go to Exit Game
		if(UIM.CreateButton(Screen.width * 0.45f, Screen.height * 0.65f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.exit))
		{
			SM.MakeSound(SM.button_click, transform.position);
			Application.Quit();
			//AutoFade.LoadLevel("Gameover", 1.0f, 1.0f, Color.black);
		}

		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(UIM.CreateButton(Screen.width * 0.01f, Screen.height * 0.88f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.back, "BACKBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.Escape))
		{
			SM.MakeSound(SM.button_click, transform.position);
			int splashscreen = PlayerPrefs.GetInt("splashscreen");
			AutoFade.LoadLevel(splashscreen, 0.5f, 0.5f, Color.black);
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
			UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.82f, Screen.width * 0.1f, Screen.height * 0.05f, UIM.esc_text);
		}
	}
}
