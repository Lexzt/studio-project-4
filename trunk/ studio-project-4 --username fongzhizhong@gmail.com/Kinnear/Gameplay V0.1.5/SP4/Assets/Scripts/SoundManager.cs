using UnityEngine;
using System.Collections;

//singleton class to manage all the audio in the game
public class SoundManager : MonoBehaviour {

	//instance of the class
	public static SoundManager SoundInstance;

	//variable to store audio for button click
	public AudioClip button_click;

	//function that initializes
	void Awake()
	{	
		if (SoundInstance != null && SoundInstance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			SoundInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//function for button click sound
	public void MakeButtonClick()
	{
		MakeSound(button_click);
	}

	//function to load in and play audio
	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
		//AudioSource.PlayClipAtPoint(originalClip, position);
	}
}
