using UnityEngine;
using System.Collections;


public class AIScript : MonoBehaviour {

//	public float m_fAttackDistance = 1.5f; // Meele
//	public float m_fChaseRange = 10.0f;
//	public float m_fLookAtRange = 20.0f;

	public enum Type{
		END = 0,
		MEELE = 1,
		RANGE = 2,
	};
	public float m_fHealth;
	public Type e_Type = Type.END;
	public float EnemySpeed;
	public float Damping;
	public float AttackCDTimer;
	
	public float m_fChaseRange;
	public float m_fLookAtRange;

	public Transform[] m_playersTransform;	
	private Transform m_playerTransform;

	public float AIMeeleDamage;
	public float AIRangeDamage;
	public float AIMeeleRange;
	public float AIRangeRange;

	public CharacterController AIController;
	 	
	private float Distance;
	private float CurrentLowest;
	private float AttackTimer;
	private Vector3 moveDirection;
	private float m_fGravity = 9.81f;
	private bool m_bDead = false;


	// Use this for initialization
	void Start () {
		AttackCDTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_fHealth <= 0)
		{
			m_bDead = true;
		}

		if(m_bDead == false)
		{
			//m_playerTransform = FindCloestPlayer().transform;
			Distance = Vector3.Distance(m_playerTransform.position, transform.position);
			if(Distance < m_fLookAtRange)
			{
				LookFor();
				Debug.Log("Looking In Range!");
			}

			if(Distance > m_fLookAtRange)
			{
				renderer.material.color = Color.green;
				Debug.Log("Not In Range!");
			}

			if(Distance < AIMeeleRange && 
			   Time.time > AttackTimer &&
			   e_Type == Type.MEELE)
			{
				AttackMeele();
				Debug.Log("Attack Meele");
			}
			else if(Distance < AIRangeRange && 
			        Time.time > AttackTimer &&
			        e_Type == Type.RANGE)
			{
				AttackRange();
				Debug.Log("Attack Range");
			}
			else if(Distance < m_fChaseRange)
			{
				Chase();
				Debug.Log("Chasing!");
			}
		}
	}

	void LookFor (){
		renderer.material.color = Color.yellow;
		Quaternion rotation = Quaternion.LookRotation(m_playerTransform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
	}
	
	void AttackMeele (){
		m_playerTransform.SendMessage("ApplyDammage", AIMeeleDamage);
		Debug.Log("Meele Dmg");
		AttackTimer = Time.time + AttackCDTimer;
	}

	void AttackRange (){
		m_playerTransform.SendMessage("ApplyDammage", AIRangeDamage);
		Debug.Log("Range Dmg");
		AttackTimer = Time.time + AttackCDTimer;
	}

	void Chase (){
		renderer.material.color = Color.red;
		
		moveDirection = transform.forward;
		moveDirection *= EnemySpeed;
		
		moveDirection.y -= m_fGravity * Time.deltaTime;
		AIController.Move(moveDirection * Time.deltaTime);
	}
	
	GameObject FindCloestPlayer(){
		GameObject[] ListOfPlayers;
		ListOfPlayers = GameObject.FindGameObjectsWithTag ("Player");
		GameObject ClosestObj = null;
		float m_fDistance = Mathf.Infinity;
		Vector3 m_vPosition = transform.position;
		foreach(GameObject Obj in ListOfPlayers){
			Vector3 Diff = (Obj.transform.position - m_vPosition);
			float curDistance = Diff.sqrMagnitude;
			if(curDistance < m_fDistance){
				ClosestObj = Obj;
				m_fDistance = curDistance;
			}
		}
		return ClosestObj;
	}
}
