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

	// Use this for initialization
	void Start () {
		btnX = Screen.width * (float)0.05;
		btnY = Screen.height * (float)0.05;
		btnW = Screen.width * (float)0.1;
		btnH = Screen.height * (float)0.1;
	}

	void startServer() {
		Network.InitializeServer (10, 25001, !Network.HavePublicAddress ());
		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.port = 23466;
		MasterServer.RegisterHost(gameName,"Testing Server Name", "This is a Notification");
	}

	void refreshHostList(){
		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.port = 23466;
		MasterServer.RequestHostList (gameName);
		refreshing = true;
	}

	void spawnPlayer () {
		Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);
	}

	void OnServerInitialized(){
		Debug.Log ("Server Initalize");
		spawnPlayer ();
	}

	void OnConnectedToServer(){
		spawnPlayer ();
	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log("Registered Server!");
		}
	}

	void OnGUI () {
		if(!Network.isClient && !Network.isServer){
			if (GUI.Button (new Rect (btnX, btnY, btnW, btnH), "Start Server")) {
				Debug.Log ("Starting Server");
				startServer();
			}

			if (GUI.Button (new Rect (btnX, btnY * (float)1.2 + btnH, btnW, btnH), "Refresh Host")) {
				Debug.Log ("Refreshing Host");
				refreshHostList();
			}

			if(hostData != null){
				for(int i = 0; i < hostData.Length; i++){
					if (GUI.Button (new Rect (btnX * (float)1.5 + btnW, 
					                          btnY * (float)1.2 + (btnH * i), 
					                          btnW * (float)3, 
					                          btnH * (float)0.5), 
					                hostData[i].gameName)) {
						Network.Connect(hostData[i]);
					}
				}
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
}
