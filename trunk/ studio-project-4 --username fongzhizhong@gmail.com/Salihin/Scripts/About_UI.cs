using UnityEngine;
using System.Collections;

public class About_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;
	
	//variable to store text upon mouse hover of BACK, LEFT and RIGHT button
	private string BackButtonTxt, LeftButtonTxt, RightButtonTxt;

	//variables to enable sliding to the left or right
	private bool slideleft, slideright;

	//variables to store current position of the panels and it's offset
	private float currpanel1x, currpanel2x, offsetx;

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

		slideleft = slideright = false;

		offsetx = 0.0f;

		currpanel1x = (Screen.width * 0.1f) + offsetx;
		currpanel2x = (Screen.width * 0.8f) + offsetx;

		trsMatrix = Matrix4x4.identity;
		positionVec = Vector3.zero;
	}

	void Update()
	{
		//If the slideleft boolean is true
		if(slideright)
		{
			//Interpolate the current vector x component until it has the same as value the screen width
			positionVec.x = Mathf.SmoothStep(positionVec.x, -Screen.width, Time.deltaTime * 10);
			/*Make 'trsMatrix' a matrix that translates, rotates and scales the GUI. 
            The position is set to positionVec, the Quaternion is set to identity 
            and the scale is set to one.*/
			trsMatrix.SetTRS(positionVec, Quaternion.identity, Vector3.one); 
		}
		else if(slideleft)
		{
			//Interpolate the current vector x component until it reaches zero
			positionVec.x = Mathf.SmoothStep(positionVec.x, 0.0f, Time.deltaTime * 10);
			/*Make 'trsMatrix' a matrix that translates, rotates and scales the GUI. 
            The position is set to positionVec, the Quaternion is set to identity 
            and the scale is set to one.*/
			trsMatrix.SetTRS(positionVec, Quaternion.identity, Vector3.one);
		}
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//draw mainmenu background texture
		UIM.CreateTexture(UIM.about_background, 0.0f, 0.0f, Screen.width, Screen.height, false);

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
			UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.82f, Screen.width * 0.1f, Screen.height * 0.05f, BackButtonTxt);
		}

		//if button is clicked or Left Arrow key is pressed, slide panel to the left
		if(UIM.CreateButton(Screen.width * 0.01f, Screen.height * 0.45f, Screen.width * 0.05f, Screen.height * 0.1f, UIM.left, "LEFTBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.LeftArrow))
		{
			slideleft = true;
			slideright = false;
		}

		if(GUI.tooltip == "LEFTBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.01f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, LeftButtonTxt);
		}

		//if button is clicked or Right Arrow key is pressed, slide panel to the right
		if(UIM.CreateButton(Screen.width * 0.94f, Screen.height * 0.45f, Screen.width * 0.05f, Screen.height * 0.1f, UIM.right, "RIGHTBUTTON")
		   ||
		   Input.GetKeyDown(KeyCode.RightArrow))
		{
			slideright = true;
			slideleft = false;
		}

		if(GUI.tooltip == "RIGHTBUTTON")
		{
			UIM.mouseOver = true;
		}
		else
		{
			UIM.mouseOver = false;
		}
		
		if(UIM.mouseOver)
		{
			UIM.CreateBox(Screen.width * 0.94f, Screen.height * 0.4f, Screen.width * 0.1f, Screen.height * 0.05f, RightButtonTxt);
		}

		//The GUI matrix must changed to the trsMatrix
		GUI.matrix = trsMatrix;

		CreatePanel1();
		CreatePanel2();

		//To reset to GUI matrix
		GUI.matrix = Matrix4x4.identity;
	}

	void CreatePanel1()
	{
		UIM.CreateButton(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.6f, UIM.aboutpanel1);
	}

	void CreatePanel2()
	{
		//currpanel2x += offsetx;
		UIM.CreateButton((Screen.width * 0.1f) + Screen.width, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.6f, UIM.aboutpanel2);
	}
}
