using UnityEngine;
using System.Collections;

public class Credits_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;

	private Matrix4x4 trsMatrix;
	private Vector3 positionVec;
	
	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		SplashSound = GameObject.Find("Sound");
		SM = SplashSound.GetComponent<SoundManager>();

		BackButtonTxt = "Click or Press ESC";

		trsMatrix = Matrix4x4.identity;
		positionVec = Vector3.zero;
	}

	void Update()
	{
		//Interpolate the current vector x component until it has the same as value the screen width
		positionVec.y = Mathf.SmoothStep(positionVec.y, -Screen.height, Time.deltaTime);
		/*Make 'trsMatrix' a matrix that translates, rotates and scales the GUI. 
		The position is set to positionVec, the Quaternion is set to identity 
        and the scale is set to one.*/
		trsMatrix.SetTRS(positionVec, Quaternion.identity, Vector3.one); 
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//draw mainmenu background texture
		UIM.CreateTexture(UIM.credits_background, 0.0f, 0.0f, Screen.width, Screen.height, false);

		//The GUI matrix must changed to the trsMatrix
		GUI.matrix = trsMatrix;

		CreatePanel1();

		//To reset to GUI matrix
		GUI.matrix = Matrix4x4.identity;

		//if button is clicked or Backspace key is pressed, fade into and go back to previous screen
		if(UIM.CreateButton(Screen.width * 0.01f, Screen.height * 0.88f, Screen.width * 0.1f, Screen.height * 0.1f, UIM.back, "BACKBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.Escape))
		{
			SM.MakeSound(SM.button_click, transform.position);
			int mainmenu = PlayerPrefs.GetInt("mainmenu");
			AutoFade.LoadLevel(mainmenu, 0.5f, 0.5f, Color.black);
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

	void CreatePanel1()
	{
		UIM.CreateButton(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.6f, UIM.creditspanel1);
	}
}
