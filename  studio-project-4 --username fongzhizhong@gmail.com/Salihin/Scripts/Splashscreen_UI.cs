using UnityEngine;
using System.Collections;

//class for Splashscreen's User Interface
public class Splashscreen_UI : MonoBehaviour {
	
	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("Splashscreen_UI");
		UIM = SplashUI.GetComponent<UIManager>();
	}

	//function to render User Interface
	void OnGUI ()
	{
		if(!AutoFade.Fading)
		{
			//if button is clicked or Enter key is pressed, fade into and go to Mainmenu scene
			if(UIM.CreateButton(Screen.width * 0.3f, Screen.height * 0.8f, Screen.width * 0.4f, Screen.height * 0.1f, "Press To Start")
			  ||
			   Input.GetKeyDown(KeyCode.Return))
			{
				AutoFade.LoadLevel("Mainmenu", 3.0f, 1.0f, Color.black);
			}
		}
	}
}
