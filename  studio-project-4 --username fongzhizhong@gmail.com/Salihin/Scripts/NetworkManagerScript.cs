using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

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

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;

	private bool showJoinGameInfo;

	// Use this for initialization
	void Start () {
		btnX = Screen.width * (float)0.01;
		btnY = Screen.height * (float)0.05;
		btnW = Screen.width * (float)0.1;
		btnH = Screen.height * (float)0.1;

		//assign gameobject and script
		SplashUI = GameObject.Find("Splashscreen_UI");
		UIM = SplashUI.GetComponent<UIManager>();

		BackButtonTxt = "Click or Press ESC";
		showJoinGameInfo = false;
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
		Network.InitializeServer (10, 25002, !Network.HavePublicAddress ());
		
		MasterServer.RegisterHost(gameName,"Testing Server Name", "This is a Notification");

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
	}

	void spawnPlayer () {
		Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);
		Debug.Log ("Test");
	}

	void OnServerInitialized(){
		Debug.Log ("Server Initalize");
		spawnPlayer ();
	}

	void OnConnectedToServer(){
		Debug.Log ("I am here");
		spawnPlayer ();
	}
	
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registered Server!");
		}
	}

	void OnGUI () {
		if(!Network.isClient && !Network.isServer){

			//if (GUI.Button (new Rect (btnX, btnY, btnW, btnH), "Start Server")) 
			if (UIM.CreateButton(btnX, btnY, btnW, btnH, "Start Server")) 
			{
				Debug.Log ("Starting Server");
				startServer();
			}

			//if (GUI.Button (new Rect (btnX, btnY * (float)1.2 + btnH, btnW, btnH), "Refresh Host")) 
			if (UIM.CreateButton(btnX, btnY * (float)1.2 + btnH, btnW, btnH, "Join Game")) 
			{
				showJoinGameInfo = true;
			}

			if(hostData != null){
				for(int i = 0; i < hostData.Length; i++){
					if (GUI.Button (new Rect (btnX * (float)1.5 + btnW, 
					                          btnY * (float)1.2 + (btnH * i), 
					                          btnW * (float)3, 
					                          btnH * (float)0.5), 
					                hostData[i].gameName)) {
						Error1 = Network.Connect(hostData[i].ip[0],hostData[i].port);
						Debug.Log (hostData[i].ip[0]);
						Error += Error1;
					}
				}
			}
		}

		if(Error != null)
		{
			GUI.Label (new Rect (300, 300, 200, 50),Error);
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
		
		if(UIM.mouseOver && GUI.tooltip == "BACKBUTTON")
		{
			UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.82f, Screen.width * 0.1f, Screen.height * 0.05f, BackButtonTxt);
		}

		if(showJoinGameInfo)
		{
			//IpAdress 	= GUI.TextField (new Rect (Screen.width/2 - 62, Screen.height/2, 104, 20), IpAdress, 25);
			//Port 		= GUI.TextField (new Rect (Screen.width/2 + 42, Screen.height/2, 50, 20), Port, 25);
			
			UIM.CreateBox(Screen.width * 0.01f, btnY * (float)2.4 + btnH + 25, 80, 20, "IPAddress");
			UIM.CreateBox(Screen.width * 0.075f + 105, btnY * (float)2.4 + btnH + 25, 30, 20, "Port");
			
			IpAddress 	= GUI.TextField (new Rect (Screen.width * 0.07f, btnY * (float)2.4 + btnH + 25, 105, 20), IpAddress, 25);
			Port 		= GUI.TextField (new Rect (Screen.width * 0.07f + 145, btnY * (float)2.4 + btnH + 25, 50, 20), Port, 25);
			
			if (GUI.Button (new Rect (Screen.width * 0.01f, btnY * (float)2.4 + btnH + 45, 155, 20), "Start Host"))
			{
				Debug.Log ("Start Host");
				startServer();
			}
			
			if (GUI.Button (new Rect (Screen.width * 0.01f, btnY * (float)2.4 + btnH + 65, 155, 20), "Refresh Host")) {
				Debug.Log ("Refresh Host");
				refreshHostList();
			}
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
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: "+ error);
		Error = "Could not connect to server: " + error;
	}

}
