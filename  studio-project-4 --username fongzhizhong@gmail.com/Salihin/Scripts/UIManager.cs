using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	//instance of the class
	public static UIManager UIInstance;

	//public variable to attach a texture for Start button
	public Texture startgame;

	//public variable to attach a texture for splashscreen background
	public Texture splashscreen;

	//public variable to attach a texture for bloodsplatter
	public Texture bloodsplatter;

	//public variable to attach texture for Pause overlay
	public Texture pauseTexture;

	//public variable to attach texture for gameover background
	public Texture gameoverTextuire;

	//variable to store texture's alpha
	private float alpha;
	
	//variables to store rendom position for texture
	private float randleft, randtop;
	
	//variable to reduce alpha by
	public float reduce_alpha;

	//variable to enable or disble fading of texture
	private bool fading, randomgenerate;
	
	//public variable to check if mouse is hovering a button
	public bool mouseOver;

	//variables to store Color of health bar and current health bar
	private Color healthColor, currhealthColor;

	//function that initializes
	void Awake()
	{	
		alpha = 1.0f;
		fading = randomgenerate = false;

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

	//function to create a button taking in the left and top position of the button, width and height of the button and a text for the button
	public bool CreateButton(float button_left, float button_top, float button_width, float button_height, string text, string tooltip)
	{
		//if button is clicked, return true
		//if button is not clicked, return false
		if(GUI.Button(new Rect(button_left, button_top, button_width, button_height), new GUIContent(text, tooltip)))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//function to create a button taking in the left and top position of the button, width and height of the button and a texture for the button
	public bool CreateButton(float button_left, float button_top, float button_width, float button_height, Texture2D texture, string tooltip)
	{
		//if button is clicked, return true
		//if button is not clicked, return false
		if(GUI.Button(new Rect(button_left, button_top, button_width, button_height), new GUIContent(texture, tooltip), GUIStyle.none))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void CreateBox(float box_left, float box_top, float box_width, float box_height, string text)
	{
		GUI.Box(new Rect(box_left, box_top, box_width, box_height), text);
	}

	//function to draw texture at a random position with a specific width and height
	public void CreateTexture(Texture texture, float width, float height, bool fading)
	{
		//random generate poistion once only
		if(!randomgenerate)
		{
			randleft = Random.Range(0.0f, Screen.width - width);
			randtop = Random.Range(0.0f, Screen.height - height);
			
			randomgenerate = true;
		}

		//reduce alpha of texture
		if(fading)
		{
			if(alpha >= 0.0f)
			{
				alpha -= reduce_alpha;
				GUI.color = new Color(1, 1, 1, alpha);
				GUI.DrawTexture(new Rect(randleft, randtop, width, height), texture);
			}
		}
		//draw texture
		else
		{
			GUI.DrawTexture(new Rect(randleft, randtop, width, height), texture);
		}
	}
	
	//function to draw texture at a specific position with a specific width and height
	public void CreateTexture(Texture texture, float left, float top, float width, float height, bool fading)
	{
		//reduce alpha of texture
		if(fading)
		{
			if(alpha >= 0.0f)
			{
				alpha -= reduce_alpha;
				GUI.color = new Color(1, 1, 1, alpha);
				GUI.DrawTexture(new Rect(left, top, width, height), texture);
			}
		}
		//draw texture
		else
		{
			GUI.DrawTexture(new Rect(left, top, width, height), texture);
		}
	}

	//function to draw health bar
	private void DrawRect(float left, float top, float width, float height, Color color)
	{
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0, 0, color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(new Rect(left, top, width, height), GUIContent.none);
	}

	//function to draw health bar
	public void DrawHealth2D(float left, float top, float currWidth, float maxWidth, float height)
	{
		//ADDED
		//=====
		if(currWidth == maxWidth)
		{
			healthColor = new Color(0.0f, 0.3f, 0.0f, 1.0f);
			currhealthColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
		}
		else if(currWidth < maxWidth && currWidth >= 0.3f * maxWidth)
		{
			healthColor = new Color(0.3f, 0.3f, 0.0f, 1.0f);
			currhealthColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
		}
		else if(currWidth < 0.3f * maxWidth)
		{
			healthColor = new Color(0.3f, 0.0f, 0.0f, 1.0f);
			currhealthColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		}

		//draw health bar
		DrawRect(left, top, maxWidth, height, healthColor);

		//draw current health bar
		DrawRect(left, top, currWidth, height, currhealthColor);
	}	
}
