using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class BotControlScript : MonoBehaviour
{
	[System.NonSerialized]					
	public float lookWeight;					// the amount to transition when using head look
	
	[System.NonSerialized]
	public Transform enemy;						// a transform to Lerp the camera to during head look
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	public bool useCurves;						// a setting for teaching purposes to show use of curves


	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CapsuleCollider col;					// a reference to the capsule collider of the character
	

	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");			// these integers are references to our animator's states
	static int jumpState = Animator.StringToHash("Base Layer.Jump");				// and are used to check state for various actions to occur
	static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");		// within our FixedUpdate() function below
	static int fallState = Animator.StringToHash("Base Layer.Fall");
	static int rollState = Animator.StringToHash("Base Layer.Roll");
//	static int waveState = Animator.StringToHash("Layer2.Wave");
	 
	private string currentAnimation = "";

	public bool ignoreAnimationLoop;
	void Start ()
	{
		// initialising reference variables
		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();				
//		enemy = GameObject.Find("Enemy").transform;	
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);
	}
	
	
	void FixedUpdate ()
	{
		if(networkView.isMine){
			float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
			float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
			anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
			anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
			anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
			anim.SetLookAtWeight(lookWeight);					// set the Look At Weight - amount to use look at IK vs using the head's animation
			currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

			if(anim.layerCount ==2)		
				layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation

			Debug.Log(v+" "+h);
			networkView.RPC("SetFloat",RPCMode.AllBuffered, v);
			networkView.RPC("SetDir",RPCMode.AllBuffered, h);
			networkView.RPC("SetAnimSpeed",RPCMode.AllBuffered, animSpeed);
			
			// STANDARD JUMPING
			
			// if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
			if (currentBaseState.nameHash == locoState)
			{
				if(Input.GetButtonDown("Jump"))
				{
					//anim.SetBool("Jump", true);
					//networkView.RPC("Jump",RPCMode.AllBuffered, true);
					currentAnimation = "Jump";
					anim.Play("Jump");
					networkView.RPC("SetAnimation",RPCMode.AllBuffered, currentAnimation);
				}

			}
			
			else if(currentBaseState.nameHash == jumpState)
			{
				if(!anim.IsInTransition(0))
				{
					//col.height = anim.GetFloat("ColliderHeight");
					//anim.SetBool("Jump", false);
					networkView.RPC("JumpEnd",RPCMode.AllBuffered, false,anim.GetFloat("ColliderHeight"));
				}
			}
			
			
			// JUMP DOWN AND ROLL 
			
			// if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
			// this is a slight lowering so that the collider hits the floor as the character extends his legs
			else if (currentBaseState.nameHash == jumpDownState)
			{
				col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
			}
			
			// if we are falling, set our Grounded boolean to true when our character's root 
			// position is less that 0.6, this allows us to transition from fall into roll and run
			// we then set the Collider's Height equal to the float curve from the animation clip
			else if (currentBaseState.nameHash == fallState)
			{
				col.height = anim.GetFloat("ColliderHeight");
			}
			
			// if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
			// this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
			// boxes, and then extends the collider as we come out of the roll
			// we also moderate the Y position of the collider using another of these curves on line 128
			else if (currentBaseState.nameHash == rollState)
			{
				if(!anim.IsInTransition(0))
				{
					if(useCurves)
						col.height = anim.GetFloat("ColliderHeight");
					
					col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
					
				}
			}
		}
	}

	[RPC]
	void SetAnimation (string clip){
		currentAnimation = clip;
		anim.Play (clip);
	}

	[RPC]
	void SetFloat(float newSpeed){
		anim.SetFloat("Speed", newSpeed);
	}

	[RPC]
	void SetDir(float newDir){
		anim.SetFloat("Direction", newDir);
	}

	[RPC]
	void SetAnimSpeed(float newSpeed){
		anim.speed = newSpeed;
	}
}
