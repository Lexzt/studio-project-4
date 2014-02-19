using UnityEngine;
using System.Collections;

public class ExitScreen_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	//variable to store text upon mouse hover of BACK button
	private string YesButtonTxt;

	//variable to store text upon mouse hover of BACK button
	private string NoButtonTxt;
	
	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("Splashscreen_UI");
		UIM = SplashUI.GetComponent<UIManager>();

		YesButtonTxt = "Click or Press Y";
		NoButtonTxt = "Click or Press N";
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(UIM.CreateButton(Screen.width * 0.4f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, "YES", "YESBUTTON")
		   ||
		   Input.GetKeyDown("y"))
		{
			AutoFade.LoadLevel("Mainmenu", 1.0f, 1.0f, Color.black);
		}

		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(UIM.CreateButton(Screen.width * 0.5f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, "NO", "NOBUTTON")
		   ||
		   Input.GetKeyDown("n"))
		{
			//int gameplay = PlayerPrefs.GetInt("gameplay");
			//AutoFade.LoadLevel(gameplay, 1.0f, 1.0f, Color.black);
		}

		if(GUI.tooltip == "YESBUTTON" || GUI.tooltip == "NOBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(GUI.tooltip == "YESBUTTON" && UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, YesButtonTxt);
		}
		else if(GUI.tooltip == "NOBUTTON" && UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.5f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, NoButtonTxt);
		}
	}
}
