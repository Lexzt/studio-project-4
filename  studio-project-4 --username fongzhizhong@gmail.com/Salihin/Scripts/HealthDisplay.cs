using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	public float health = 100;	
	string textDisplay = "";
	float currentHealth;


	//ADDED
	//=====
	GameObject SplashUI;
	UIManager UIM;
	//=====

	void Start()
	{
		currentHealth = health;


		//ADDED
		//=====
		SplashUI = GameObject.Find("UI");
		UIM = SplashUI.GetComponent<UIManager>();
		//=====
	}


	// Update is called once per frame
	void Update () {
		if(health <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	public void ApplyDamage(float healthDeduction)
	{
		print (transform.position);
		
		currentHealth -= healthDeduction;
		transform.position = transform.position - Camera.main.transform.forward;
		print (transform.position);
		// If the health of anything goes below the 
		if(currentHealth < 0)
		{
			currentHealth = 0;
			//transform.renderer.enabled = false;
		}
	}

	public void ApplyHeal(float healthHeal)
	{
		currentHealth += healthHeal;
		
		// If the health of anything goes below the 
		if(currentHealth > health)
		{
			currentHealth = health;
		}
	}

	void OnGUI()
	{
		//textDisplay = GUI.TextField(new Rect(10, 10, 200, 20), currentHealth.ToString());
	}
}
