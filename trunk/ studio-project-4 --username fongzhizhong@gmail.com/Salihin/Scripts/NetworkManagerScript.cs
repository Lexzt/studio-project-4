using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	static public bool m_bIsInitalized = false;

	private float btnX;
	private float btnY;
	private float btnW;
	private float btnH;

	private string gameName = "Studio_Project_4_Networking";

	private bool refreshing;
	private HostData[] hostData;

	public GameObject playerPrefab;
	public Transform spawnObject;

	private string IpAddress = "";
	private string Port = "";
	private int TextLength = 104;

	private string Error;
	private NetworkConnectionError Error1;

	public string Name = "";


	//ADDED
	//=====
	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	//variable to store text upon mouse hover of BACK button
	private string YesButtonTxt;
	
	//variable to store text upon mouse hover of BACK button
	private string NoButtonTxt;
	
	//variable to display buttons
	private bool showExitScreen;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;

	float alpha;

	public int numofplayers;

	bool showMsg, showButt;

	private string Msg;
	
	private string IPAddLabel, PortLabel, NameLabel;

	private int numofconnectedplayers;
	//=====

	// Use this for initialization
	void Start () {
		btnX = Screen.width * (float)0.05;
		btnY = Screen.height * (float)0.05;
		btnW = Screen.width * (float)0.1;
		btnH = Screen.height * (float)0.1;

//		Inst.MeeleAIFab = (GameObject)MeeleAI;
//		Inst.RangeAIFeb = (GameObject)RangeAI;


		//ADDED
		//=====
		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		YesButtonTxt = "Click or Press Y";
		NoButtonTxt = "Click or Press N";
		
		showExitScreen = false;

		BackButtonTxt = "Click or Press ESC";

		alpha = 1.0f;

		showMsg = false;
		showButt = true;

		Msg = "Waiting for more players";

		IPAddLabel = "IPADDRESS: ";
		PortLabel = "PORT: ";
		NameLabel = "NAME: ";

		numofconnectedplayers = 0;
		//=====
	}

	void startServer() {
//		MasterServer.ipAddress = "25.145.50.169";
//		MasterServer.port = 23466;
		MasterServer.ipAddress = IpAddress;
		MasterServer.port = int.Parse(Port);
		Network.natFacilitatorIP = IpAddress;
		Network.natFacilitatorPort = int.Parse(Port);
		Network.connectionTesterIP = IpAddress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.proxyIP = IpAddress;
		Network.InitializeServer (numofplayers, 25002, !Network.HavePublicAddress ());
		
		MasterServer.RegisterHost(gameName,"Testing Server Name", "This is a Notification");

		//ADDED
		//=====
		//refreshing = true;
		//=====
	}

	void refreshHostList(){
//		MasterServer.ipAddress = "127.0.0.1";
//		MasterServer.port = 23466;
		MasterServer.ipAddress = IpAddress;
		MasterServer.port = int.Parse(Port);
		Network.connectionTesterIP = IpAddress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.natFacilitatorIP = IpAddress;
		Network.natFacilitatorPort = int.Parse(Port);
		Network.connectionTesterIP = IpAddress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.proxyIP = IpAddress;
		MasterServer.RequestHostList (gameName);
		refreshing = true;

		//ADDED
		//====
		showButt = false;
		//====
	}

	void spawnPlayer () {
		GameObject NewObj = (GameObject)Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);
		NewObj.SendMessage ("SetName", Name);
		Debug.Log ("Test");
	}

	void OnServerInitialized(){
		Debug.Log ("Server Initalize");

		if(numofconnectedplayers == numofplayers)
		{
			m_bIsInitalized = true;
			spawnPlayer();
		}
		else if(numofconnectedplayers < numofplayers)
		{
			showMsg = true;
		}
	}

	void OnConnectedToServer(){
		Debug.Log ("I am here");

		//ADDED
		//=====
		if(numofconnectedplayers == numofplayers)
		{
			spawnPlayer ();
		}
		else if(numofconnectedplayers < numofplayers)
		{
			showMsg = true;
		}
		//=====
	}
	
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registered Server!");
		}
	}

//	void OnPlayerConnected()
//	{
//		refreshing = true;
//		numofconnectedplayers = Network.connections.Length;
//
//		if(numofconnectedplayers == numofplayers)
//		{
//			showMsg = false;
//		}
//	}

	void OnGUI () {
		if(!Network.isClient && !Network.isServer){
//			IpAddress 	= GUI.TextField (new Rect (Screen.width/2 - 62, Screen.height/2, 104, 20), IpAddress, 25);
//			Port 		= GUI.TextField (new Rect (Screen.width/2 + 42, Screen.height/2, 50, 20), Port, 25);
//			Name 		= GUI.TextField (new Rect (Screen.width/2 - 62, Screen.height/2 - 20, 154, 20), Name, 25);

			//ADDED
			//=====
			if(showButt)
			{
				//box for NAME
				UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.04f, NameLabel);

				//box for IPADDRESS
				UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.44f, Screen.width * 0.1f, Screen.height * 0.04f, IPAddLabel);

				//box for PORT
				UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.48f, Screen.width * 0.1f, Screen.height * 0.04f, PortLabel);
				//=====

				Name 		= GUI.TextField (new Rect (Screen.width * 0.5f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.04f), Name, 25);
				IpAddress 	= GUI.TextField (new Rect (Screen.width * 0.5f, Screen.height * 0.44f, Screen.width * 0.1f, Screen.height * 0.04f), IpAddress, 25);
				Port 		= GUI.TextField (new Rect (Screen.width * 0.5f, Screen.height * 0.48f, Screen.width * 0.1f, Screen.height * 0.04f), Port, 25);

				if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.52f, Screen.width * 0.2f, Screen.height * 0.04f), "Start Host")) {
					Debug.Log ("Start Host");

					startServer();
				}
				
				if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.56f, Screen.width * 0.2f, Screen.height * 0.04f), "Refresh Host")) {
					Debug.Log ("Refresh Host");

					refreshHostList();
				}
			}
			else
			{
				if(hostData != null){
					for(int i = 0; i < hostData.Length; i++){
						if (GUI.Button (new Rect (Screen.width * 0.4f, 
						                          Screen.height * 0.5f, 
						                          Screen.width * 0.2f, 
						                          Screen.height * 0.05f), 
						                hostData[i].gameName)) {
							Error1 = Network.Connect(hostData[i].ip[0],hostData[i].port);
							Debug.Log (hostData[i].ip[0]);
							Error += Error1;
						}
					}
				}
			}

			//ADDED
			//=====
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
			
			if(UIM.mouseOver && GUI.tooltip == "BACKBUTTON")
			{
				UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.82f, Screen.width * 0.1f, Screen.height * 0.05f, BackButtonTxt);
			}
			//=====
		}
		//ADDED
		//=====
		else if(Network.isClient || Network.isServer)
		{
			if(showExitScreen)
			{
				GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), UIM.pauseTexture);
				
				//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
				if(	UIM.CreateButton(Screen.width * 0.4f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, "YES", "YESBUTTON")
				   ||
				   Input.GetKeyDown("y"))
				{
					Network.Disconnect();
					//DestroyObject(LobbyScript);
					DestroyObject(this);
					AutoFade.LoadLevel("Mainmenu", 1.0f, 1.0f, Color.black);
				}
				
				//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
				if(UIM.CreateButton(Screen.width * 0.5f, Screen.height * 0.45f, Screen.width * 0.1f, Screen.height * 0.1f, "NO", "NOBUTTON")
				   ||
				   Input.GetKeyDown("n"))
				{
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
					UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, YesButtonTxt);
				}
				else if(GUI.tooltip == "NOBUTTON" && UIM.mouseOver)
				{
					UIM.CreateBox(Screen.width * 0.5f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, NoButtonTxt);
				}
			}

			if(showMsg)
			{
				UIM.CreateBox(Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.05f, Msg);
			}
		}
		//=====

		if(Error != null)
		{
			GUI.Label (new Rect (300, 300, 200, 50),Error);
		}
	}
	
	// Update is called once per frame
	void Update () {
 		if (refreshing) {
			if(MasterServer.PollHostList ().Length != 0){
				Debug.Log (MasterServer.PollHostList ().Length);
				refreshing = false;
				hostData = MasterServer.PollHostList();
			}
		}

		//ADDED
		//=====
		//numofconnectedplayers = Network.connections.Length;
		//Debug.Log("numofconnectedplayers: " + numofconnectedplayers);
		
		//ADDED
		//=====
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
		//=====
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: "+ error);
		Error = "Could not connect to server: " + error;
	}
}
/*
We're no strangers to love 
You know the rules and so do I 
A full commitment's what I'm thinking of 
You wouldn't get this from any other guy 
I just wanna tell you how I'm feeling 
Gotta make you understand

Never gonna give you up, 
Never gonna let you down 
Never gonna run around and desert you 
Never gonna make you cry, 
Never gonna say goodbye 
Never gonna tell a lie and hurt you 

We've known each other for so long 
Your heart's been aching but you're too shy to say it 
Inside we both know what's been going on 
We know the game and we're gonna play it 
And if you ask me how I'm feeling 
Don't tell me you're too blind to see

Never gonna give you up, 
Never gonna let you down 
Never gonna run around and desert you 
Never gonna make you cry, 
Never gonna say goodbye 
Never gonna tell a lie and hurt you 

(Ooh give you up) 
(Ooh give you up) 
(Ooh) never gonna give, never gonna give 
(give you up) 
(Ooh) never gonna give, never gonna give 
(give you up) 

We've known each other for so long 
Your heart's been aching but you're too shy to say it 
Inside we both know what's been going on 
We know the game and we're gonna play it

We're no strangers to love 
You know the rules and so do I 
A full commitment's what I'm thinking of 
You wouldn't get this from any other guy 
I just wanna tell you how I'm feeling 
Gotta make you understand

Never gonna give you up, 
Never gonna let you down 
Never gonna run around and desert you 
Never gonna make you cry, 
Never gonna say goodbye 
Never gonna tell a lie and hurt you 
*/
