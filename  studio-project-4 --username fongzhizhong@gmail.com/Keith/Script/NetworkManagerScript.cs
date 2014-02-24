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

	private string IpAdress = "";
	private string Port = "";
	private int TextLength = 104;

	private string Error;
	private NetworkConnectionError Error1;

	public string Name = "";

	// Use this for initialization
	void Start () {
		btnX = Screen.width * (float)0.05;
		btnY = Screen.height * (float)0.05;
		btnW = Screen.width * (float)0.1;
		btnH = Screen.height * (float)0.1;
	}

	void startServer() {
//		MasterServer.ipAddress = "25.145.50.169";
//		MasterServer.port = 23466;
		MasterServer.ipAddress = IpAdress;
		MasterServer.port = int.Parse(Port);
		Network.natFacilitatorIP = IpAdress;
		Network.natFacilitatorPort = int.Parse(Port);
		Network.connectionTesterIP = IpAdress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.proxyIP = IpAdress;
		Network.InitializeServer (10, 25002, !Network.HavePublicAddress ());
		
		MasterServer.RegisterHost(gameName,"Testing Server Name", "This is a Notification");

	}

	void refreshHostList(){
//		MasterServer.ipAddress = "127.0.0.1";
//		MasterServer.port = 23466;
		MasterServer.ipAddress = IpAdress;
		MasterServer.port = int.Parse(Port);
		Network.connectionTesterIP = IpAdress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.natFacilitatorIP = IpAdress;
		Network.natFacilitatorPort = int.Parse(Port);
		Network.connectionTesterIP = IpAdress;
		Network.connectionTesterPort = int.Parse(Port);
		Network.proxyIP = IpAdress;
		MasterServer.RequestHostList (gameName);
		refreshing = true;
	}

	void spawnPlayer () {
		GameObject NewObj = (GameObject)Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);
		NewObj.SendMessage ("SetName", Name);
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
			IpAdress 	= GUI.TextField (new Rect (Screen.width/2 - 62, Screen.height/2, 104, 20), IpAdress, 25);
			Port 		= GUI.TextField (new Rect (Screen.width/2 + 42, Screen.height/2, 50, 20), Port, 25);
			Name 		= GUI.TextField (new Rect (Screen.width/2 - 62, Screen.height/2 - 20, 154, 20), Name, 25);

			if (GUI.Button (new Rect (Screen.width/2 - 62, Screen.height/2 + 20, 154, 20), "Start Host")) {
				Debug.Log ("Start Host");
				startServer();
			}
			
			if (GUI.Button (new Rect (Screen.width/2 - 62, Screen.height/2 + 40, 154, 20), "Refresh Host")) {
				Debug.Log ("Refresh Host");
				refreshHostList();
			}

//			if (GUI.Button (new Rect (btnX, btnY, btnW, btnH), "Start Server")) {
//				Debug.Log ("Starting Server");
//				startServer();
//			}
//
//			if (GUI.Button (new Rect (btnX, btnY * (float)1.2 + btnH, btnW, btnH), "Refresh Host")) {
//				Debug.Log ("Refreshing Host");
//				refreshHostList();
//			}

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
