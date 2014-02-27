using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	//instance of the class
	public static HUDManager HUDInstance;

	//variables to store the various textures
	public Texture bloodsplatter;

	//variable to store texture's alpha
	private float alpha;

	//variables to store rendom position for texture
	private float randleft, randtop;

	//variable to enable or disble fading of texture
	private bool fading, randomgenerate;

	//variable to reduce alpha by
	public float reduce_alpha;

	// Use this for initialization
	void Start () {
		if (HUDInstance != null && HUDInstance != this) 
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			HUDInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		alpha = 1.0f;
		fading = randomgenerate = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	//function to draw texture at a random position with a specific width and height
	void CreateTexture(Texture texture, float width, float height, bool fading)
	{
		if(!randomgenerate)
		{
			randleft = Random.Range(0.0f, Screen.width - width);
			randtop = Random.Range(0.0f, Screen.height - height);

			randomgenerate = true;
		}

		if(fading)
		{
			if(alpha >= 0.0f)
			{
				Debug.Log("hello");
				alpha -= reduce_alpha;
				GUI.color = new Color(1, 1, 1, alpha);
				GUI.DrawTexture(new Rect(randleft, randtop, width, height), texture);
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(randleft, randtop, width, height), texture);
		}
	}

	//function to draw texture at a specific position with a specific width and height
	void CreateTexture(Texture texture, float left, float top, float width, float height, bool fading)
	{
		if(fading)
		{
			if(alpha >= 0.0f)
			{
				alpha -= reduce_alpha;
				GUI.color = new Color(1, 1, 1, alpha);
				GUI.DrawTexture(new Rect(left, top, width, height), texture);
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(left, top, width, height), texture);
		}
	}
	
	void OnGUI()
	{
		CreateTexture(bloodsplatter, 0.0f, 0.0f, Screen.width * 0.1f, Screen.height * 0.1f, true);
	}
}
