using UnityEngine;
using System.Collections;

public class Gameplay_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;

	GameObject LobbyScript;
	NetworkManagerScript NMS;
	
	//variable to store text upon mouse hover of BACK button
	private string YesButtonTxt;

	//variable to store text upon mouse hover of BACK button
	private string NoButtonTxt;

	//variable to display buttons
	private bool showExitScreen;

	public Texture pauseTexture;

	//public Transform SpawnPoint;
	//public GameObject playerPrefab;
	
	// Use this for initialization
	void Start()
	{
		YesButtonTxt = "Click or Press Y";
		NoButtonTxt = "Click or Press N";

		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		SplashSound = GameObject.Find("Sound");
		SM = SplashSound.GetComponent<SoundManager>();

		LobbyScript = GameObject.Find("Lobby_Script");
		NMS = LobbyScript.GetComponent<NetworkManagerScript>();

		showExitScreen = false;

		//NMS.spawnPlayer();

		//if(Network.isClient)
		//{
			//NMS.OnConnectedToServer();
		//}
		//else if(Network.isServer)
		//{
			//NMS.OnServerInitialized();
		//}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!showExitScreen)
			{
				showExitScreen = true;
			}
			else
			{
				showExitScreen = false;
			}
		}
	}
	//function to render User Interface
	void OnGUI ()
	{
		if(showExitScreen)
		{
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), UIM.pauseTexture);
			UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.04f, UIM.pause_msg);
			
			//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
			if(	UIM.CreateButton(Screen.width * 0.4f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.yes, "YESBUTTON")
			   ||
			   Input.GetKeyDown("y"))
			{
				SM.MakeSound(SM.button_click, transform.position);
				//Network.Disconnect();
				//DestroyObject(LobbyScript);
				//DestroyObject(this);
				AutoFade.LoadLevel("Mainmenu", 0.5f, 0.5f, Color.black);
			}
			
			//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
			if(UIM.CreateButton(Screen.width * 0.5f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.no, "NOBUTTON")
			   ||
			   Input.GetKeyDown("n"))
			{
				SM.MakeSound(SM.button_click, transform.position);
				showExitScreen = false;
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
				//YesButtonTxt = "Click or Press Y";
				UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, UIM.yes_text);
			}
			else if(GUI.tooltip == "NOBUTTON" && UIM.mouseOver)
			{
				//NoButtonTxt = "Click or Press N";
				UIM.CreateBox(Screen.width * 0.5f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, UIM.no_text);
			}
		}
	}
}
