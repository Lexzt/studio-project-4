using UnityEngine;
using System.Collections;

//class for Splashscreen's User Interface
public class Splashscreen_UI : MonoBehaviour {

	//public variable to attach a texture for Start button
	public Texture2D startgame;

	//function to create a button taking in the left and top position of the button, width and height of the button and a text for the button
	bool CreateButton(float button_left, float button_top, float button_width, float button_height, string text)
	{
		//if button is clicked, return true
		//if button is not clicked, return false
		if(GUI.Button(new Rect(button_left, button_top, button_width, button_height), text))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//function to render User Interface
	void OnGUI ()
	{
		//if button is clicked or Enter key is pressed, go to Test scene
		if(CreateButton(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.8f, Screen.height * 0.1f, "Press To Start")
		  ||
		   Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel ("Mainmenu");
		}
	}
}
