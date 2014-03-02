using UnityEngine;
using System.Collections;

public class Gameover_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;

	//variable to store text upon mouse hover of BACK button
	private string ContinueButtonTxt;

	// Use this for initialization
	void Start () {
	
		ContinueButtonTxt = "Click or Press Enter";

		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		SplashSound = GameObject.Find("Sound");
		SM = SplashSound.GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), UIM.gameover_background);
		UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.04f, "GAMEOVER");
		
		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(	UIM.CreateButton(Screen.width * 0.4f, Screen.height * 0.45f, Screen.width * 0.2f, Screen.height * 0.1f, UIM.continue_btn, "CONTINUEBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.Return))
		{
			SM.MakeSound(SM.button_click, transform.position);
			AutoFade.LoadLevel("Mainmenu", 0.5f, 0.5f, Color.black);
		}

		if(GUI.tooltip == "CONTINUEBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(GUI.tooltip == "CONTINUEBUTTON" && UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.05f, UIM.enter_text);
		}
	}
}
