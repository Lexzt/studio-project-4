using UnityEngine;
using System.Collections;

public class Options_UI : MonoBehaviour {

	//variables to store gameobject and script
	GameObject SplashUI;
	UIManager UIM;

	GameObject SplashSound;
	SoundManager SM;

	//variable to store text upon mouse hover of BACK button
	private string BackButtonTxt;

	//variable to store volume of Music and Sound Effects audio
	int MusicVolume, SoundEffVolume;

	Color MBarColor, SEBarColor;
	
	// Use this for initialization
	void Start()
	{
		//assign gameobject and script
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();

		SplashSound = GameObject.Find("Sound");
		SM = SplashSound.GetComponent<SoundManager>();

		BackButtonTxt = "Click or Press ESC";

		MusicVolume = (int)(SM.audio.volume * 10);
		SoundEffVolume = (int)(SM.SoundEffVolume * 10);

		MBarColor = SEBarColor = Color.clear;
	}

	void Update()
	{
	}
	
	//function to render User Interface
	void OnGUI ()
	{
		//draw mainmenu background texture
		UIM.CreateTexture(UIM.options_background, 0.0f, 0.0f, Screen.width, Screen.height, false);

		//if button is clicked or Escape key is pressed, fade into and go back to previous screen
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

		AdjustMusic();
		AdjustSoundEffects();
	}

	void AdjustMusic()
	{
		UIM.CreateBox(Screen.width * 0.2f, Screen.height * 0.3f, Screen.width * 0.1f, Screen.height * 0.05f, UIM.music);
		MusicVolume = (int)(SM.audio.volume * 10);

		if(MusicVolume <= 3)
		{
			MBarColor = Color.red;
		}
		else if(MusicVolume > 3 && MusicVolume <= 5)
		{
			MBarColor = Color.yellow;
		}
		else if(MusicVolume > 5)
		{
			MBarColor = Color.green;
		}

		if(UIM.CreateButton(Screen.width * 0.7f, Screen.height * 0.3f, Screen.width * 0.05f, Screen.height * 0.025f, UIM.up))
		{
			SM.MakeSound(SM.button_click, transform.position);
			SM.audio.volume += 0.1f;
			MusicVolume = (int)(SM.audio.volume * 10);
		}
		else if(UIM.CreateButton(Screen.width * 0.7f, Screen.height * 0.325f, Screen.width * 0.05f, Screen.height * 0.025f, UIM.down))
		{
			SM.MakeSound(SM.button_click, transform.position);
			SM.audio.volume -= 0.1f;
			MusicVolume = (int)(SM.audio.volume * 10);
		}

		if(MusicVolume > 0)
		{
			for(int i = 1; i <= MusicVolume; i++)
			{
				UIM.DrawRect(Screen.width * 0.4f + (Screen.width * 0.02f * (i - 1)), Screen.height * 0.3f, Screen.width * 0.01f , Screen.height * 0.05f, MBarColor);
			}
		}
	}

	void AdjustSoundEffects()
	{
		UIM.CreateBox(Screen.width * 0.2f, Screen.height * 0.65f, Screen.width * 0.1f, Screen.height * 0.05f, UIM.sfx);
		SoundEffVolume = (int)(SM.SoundEffVolume * 10);

		if(SoundEffVolume <= 3)
		{
			SEBarColor = Color.red;
		}
		else if(SoundEffVolume > 3 && MusicVolume <= 5)
		{
			SEBarColor = Color.yellow;
		}
		else if(SoundEffVolume > 5)
		{
			SEBarColor = Color.green;
		}

		if(UIM.CreateButton(Screen.width * 0.7f, Screen.height * 0.65f, Screen.width * 0.05f, Screen.height * 0.025f, UIM.up))
		{
			SM.MakeSound(SM.button_click, transform.position);
			SM.SoundEffVolume += 0.1f;
			if(SM.SoundEffVolume >= 1.0f)
			{
				SM.SoundEffVolume = 1.0f;
			}
			SoundEffVolume = (int)(SM.SoundEffVolume * 10);
		}
		else if(UIM.CreateButton(Screen.width * 0.7f, Screen.height * 0.675f, Screen.width * 0.05f, Screen.height * 0.025f, UIM.down))
		{
			SM.MakeSound(SM.button_click, transform.position);
			SM.SoundEffVolume -= 0.1f;
			if(SM.SoundEffVolume <= 0.0f)
			{
				SM.SoundEffVolume = 0.0f;
			}
			SoundEffVolume = (int)(SM.SoundEffVolume * 10);
		}

		if(SoundEffVolume > 0)
		{
			for(int i = 1; i <= SoundEffVolume; i++)
			{
				UIM.DrawRect(Screen.width * 0.4f + (Screen.width * 0.02f * (i - 1)), Screen.height * 0.65f, Screen.width * 0.01f , Screen.height * 0.05f, SEBarColor);
			}
		}
	}
}
