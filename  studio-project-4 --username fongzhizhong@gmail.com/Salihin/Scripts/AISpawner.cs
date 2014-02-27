using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour {

	public GameObject MeeleAIFab;
	public GameObject RangeAIFeb;

	public int TotalAmountAI;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isServer && NetworkManagerScript.m_bIsInitalized)
		{
			for(int i = 0; i < TotalAmountAI; i++)
			{
				Debug.Log("Here!");
				int MeeleOrRange = Random.Range (0, 2);
				if(MeeleOrRange == 0)
				{
					Network.Instantiate(MeeleAIFab,new Vector3(Random.Range(10,250),10,Random.Range(10,250)),Quaternion.identity,1);
					Debug.Log("Here!");
				}
				else if(MeeleOrRange == 1)
				{
					Network.Instantiate(RangeAIFeb,new Vector3(Random.Range(10,250),10,Random.Range(10,250)),Quaternion.identity,1);
					Debug.Log("Here!");
				}
			}
			NetworkManagerScript.m_bIsInitalized = false;
		}
	}
}
