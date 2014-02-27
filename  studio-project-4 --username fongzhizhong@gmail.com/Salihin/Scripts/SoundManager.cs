using UnityEngine;
using System.Collections;

//singleton class to manage all the audio in the game
public class SoundManager : MonoBehaviour {

	//instance of the class
	public static SoundManager SoundInstance;

	//variable to store audio for button click
	public AudioClip button_click;

	//variables to store audio related to character
	public AudioClip foot_steps, limping_fs, got_attacked, char_attack;

	//variables to store audio related to assets
	public AudioClip door, wood, dropping, clock;

	//variables to store audio related to the environment
	public AudioClip creeping, wind;

	//variables to store audio related to the cinematics
	public AudioClip narration;

	//variables to store audio related to enemy
	public AudioClip growl1, growl2, growl3, growl4, jumping, enemy_attack;

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

	//function for foot steps sound
	public void MakeFootSteps()
	{
		MakeSound(foot_steps);
	}

	//function for limping foot steps sound
	public void MakeLimping()
	{
		MakeSound(limping_fs);
	}

	//function for got attacked sound
	public void MakeGotAttacked()
	{
		MakeSound(got_attacked);
	}

	//function for character attack sound
	public void MakeCharAttack()
	{
		MakeSound(char_attack);
	}

	//function for door sound
	public void MakeDoor()
	{
		MakeSound(door);
	}

	//function for wood sound
	public void MakeWood()
	{
		MakeSound(wood);
	}

	//function for dropping sound
	public void MakeDropping()
	{
		MakeSound(dropping);
	}

	//function for clock sound
	public void MakeClock()
	{
		MakeSound(clock);
	}

	//function for creeping sound
	public void MakeCreeping()
	{
		MakeSound(creeping);
	}

	//function for wind sound
	public void MakeWind()
	{
		MakeSound(wind);
	}

	//function for narration sound
	public void MakeNarration()
	{
		MakeSound(narration);
	}	

	//function for growl sounds
	public void MakeGrowl(int growlnum)
	{
		if(growlnum == 1)
		{
			MakeSound(growl1);
		}
		else if(growlnum == 2)
		{
			MakeSound(growl2);
		}
		else if(growlnum == 3)
		{
			MakeSound(growl3);
		}
		else if(growlnum == 4)
		{
			MakeSound(growl4);
		}
	}

	//function for jumping sound
	public void MakeJumping()
	{
		MakeSound(jumping);
	}

	//function for enemy attack sound
	public void MakeEnemyAttack()
	{
		MakeSound(enemy_attack);
	}

	//function to load in and play audio
	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
		//AudioSource.PlayClipAtPoint(originalClip, position);
	}
}
