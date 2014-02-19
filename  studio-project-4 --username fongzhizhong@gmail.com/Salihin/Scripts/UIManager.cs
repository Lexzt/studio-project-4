using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	//instance of the class
	public static UIManager UIInstance;

	//public variable to attach a texture for Start button
	public Texture2D startgame;
	
	//function that initializes
	void Awake()
	{	
		if (UIInstance != null && UIInstance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			UIInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	//function to create a button taking in the left and top position of the button, width and height of the button and a text for the button
	public bool CreateButton(float button_left, float button_top, float button_width, float button_height, string text)
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
}
