using UnityEngine;
using System.Collections;

public class ChatScript : MonoBehaviour 
{
	public GUISkin myskin;
	
	private Rect windowRect = new Rect(200, 200, 300, 450);
	private string messBox = "", messageToSend = "", user = "";

	private bool m_bKeyPress = false;

	private BotControlScript TempObj;

	private bool enter = false;
	private Vector2 scrollPosition;
	void Start ()
	{
	}

	private void OnGUI()
	{
		GUI.skin = myskin;

		if(m_bKeyPress == true)
		{
			if (NetworkPeerType.Disconnected != Network.peerType)
				windowRect = GUI.Window(1, windowRect, windowFunc, "Chat");
		}
	}	
	
	private void windowFunc(int id)
	{
		scrollPosition = GUILayout.BeginScrollView (scrollPosition);
		GUILayout.Box(messBox, GUILayout.Height(400));
		GUILayout.EndScrollView ();

		GUILayout.BeginHorizontal();

		if (Event.current.Equals (Event.KeyboardEvent ("return")))
		{
			enter = true;
		}

		messageToSend = GUILayout.TextField(messageToSend);
		if (GUILayout.Button("Send" , GUILayout.Width(75)) || enter)
		{
			networkView.RPC("SendMessage", RPCMode.All, user + ": " + messageToSend + "\n");
			messageToSend = "";
			enter = false;
		}
		GUILayout.EndHorizontal();
		
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("User:");
//
//		user = GUILayout.TextField(user);
//
//		GUILayout.EndHorizontal();
		
		GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
	
	[RPC]
	private void SendMessage(string mess)
	{
		messBox += mess;
	}

	void Update ()
	{	
		if(user != "")
		{
			if(Input.GetKeyDown(KeyCode.T))
			{
				if(m_bKeyPress)
				{
					m_bKeyPress = false;
				}
				else
				{
					m_bKeyPress = true;
				}
			}

			TempObj = GameObject.Find ("Robot(Clone)").GetComponent<BotControlScript> ();
			if(TempObj != null)
			{
				user = TempObj.m_sName;
			}
		}
	}
}
