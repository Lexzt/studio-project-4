using UnityEngine;
using System.Collections;

//singleton class to manage all the audio in the game
public class SoundEffectsHelper : MonoBehaviour {

	//instance of the class
	public static SoundEffectsHelper Instance;

	//variable to store audio for button click
	public AudioClip button_click;

	//function that initializes
	void Awake()
	{	
		if (Instance != null && Instance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			Instance = this;
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
