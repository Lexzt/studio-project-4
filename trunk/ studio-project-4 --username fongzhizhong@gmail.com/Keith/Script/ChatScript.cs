using UnityEngine;
using System.Collections;

class ChatEntry
{
	public string Name = "";
	public string Text = "";
}

public class ChatScript : MonoBehaviour {

	public bool m_bUsingChat = false;
	public GUISkin m_Skin;
	public bool m_bShowChat = true;

	private string 	InputField = "";

	private Vector2 ScrollPos;
	public float 	m_iWidth;
	public float 	m_iHeight;
	private string 	m_sPlayerName;
	private float 	m_fLastUnFocus;
	private Rect	m_rWindow;
	private float	m_fLastEntry;
	private NetworkView m_NnetView;

	private ChatScript TempChat;
	private ArrayList chatEntries = new ArrayList ();
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		m_bUsingChat = false;

		m_NnetView = networkView;
		TempChat = this;

		m_rWindow = new Rect (Screen.width / 2, Screen.height / 2, 300, 100);
		m_fLastEntry = Time.time;

		m_sPlayerName = "Keith";
		if(m_sPlayerName == ""){
			m_sPlayerName = "RandomName "+Random.Range(1,999);
		}
	}

	void ShowChat(){
		m_bShowChat = true;
		InputField = "";
		chatEntries = new ArrayList ();
	}

	void HideChat(){
		m_bShowChat = false;
		InputField = "";
		chatEntries = new ArrayList ();
	}

	void OnGUI() {
		if(!m_bShowChat)
			return;

		GUI.skin = m_Skin;

		if(Event.current.type == EventType.keyDown &&
		   string.Equals(Event.current.character,"\n") &&
		   InputField.Length <= 0){
			if(m_fLastUnFocus+0.5 <Time.time){
				m_bUsingChat = true;
				GUI.FocusWindow(5);
				GUI.FocusControl("Chat input field");
				Screen.lockCursor = false;
			}
		}

		if(Time.time - m_fLastEntry > 10 && Network.isServer){
			addGameChatMessage(" ");
			m_fLastEntry = Time.time;
		}

		m_rWindow = GUI.Window (5, m_rWindow, GlobalChatWindow, "");
	}

	void GlobalChatWindow(int id){
		GUILayout.BeginVertical ();
		GUILayout.Space (10);
		GUILayout.EndVertical ();

		ScrollPos = GUILayout.BeginScrollView (ScrollPos);

		foreach(ChatEntry entry in chatEntries){
			GUILayout.BeginHorizontal();
			if(entry.Name == ""){
				GUILayout.Label(entry.Text);
			}else{
				GUILayout.Label (entry.Name+": "+entry.Text);
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(3);
		}
		GUILayout.EndScrollView ();

		if (Event.current.type == EventType.keyDown && 
		    string.Equals(Event.current.character,"\n") &&
		    InputField.Length > 0)
		{
			HitEnter(InputField);
		}
		
		else if (Event.current.type == EventType.keyDown && 
		         string.Equals(Event.current.character,"\n") &&
		         InputField.Length == 0){
			InputField = ""; //Clear line
			GUI.UnfocusWindow ();//Deselect chat
			m_fLastUnFocus=Time.time;
			m_bUsingChat=false;
			Screen.lockCursor = true;
		}
		GUI.SetNextControlName("Chat input field");
		InputField = GUILayout.TextField(InputField);

		if(Input.GetKeyDown("mouse 0")){
			if(m_bUsingChat){
				m_bUsingChat=false;
				GUI.UnfocusWindow ();//Deselect chat
				m_fLastUnFocus=Time.time;
			}
		}
	}

		void HitEnter(string msg){
			msg = msg.Replace("\n", "");
			m_NnetView.RPC("ApplyGlobalChatText", RPCMode.All, m_sPlayerName, msg);
			InputField = ""; //Clear line
			GUI.UnfocusWindow ();//Deselect chat
			m_fLastUnFocus=Time.time;
			m_bUsingChat=false;
			Screen.lockCursor = true;
			
		}
		
		void StaticMsg(string msg){
			HitEnter(msg);
		}
		
		[RPC]
		void ApplyGlobalChatText (string name, string msg)
		{
			ChatEntry entry = new ChatEntry();
			entry.Name = name;
			entry.Text = msg;
			
			chatEntries.Add(entry);
			m_fLastEntry = Time.time;
			
			//Remove old entries
			if (chatEntries.Count > 4){
				chatEntries.RemoveAt(0);
			}
			ScrollPos.y = 1000000; 
		}
		
		
		
		//Add game messages etc
		void addGameChatMessage(string str){
			ApplyGlobalChatText("", str);
			if(Network.connections.Length>0){
				networkView.RPC("ApplyGlobalChatText", RPCMode.Others, "", str);    
			}   
		}
	}
	