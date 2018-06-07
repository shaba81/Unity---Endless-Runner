using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float speedMultiplier;
	public float speedIncreaseMilestone;
	public float jumpForce;
	public float jumpTime;
	public bool grounded;
	public LayerMask whatIsGround; //selezione del layer che ho inserito su Unity
	public Transform groundCheck;
	public float groundCheckRadius;
	public GameManager theGameManager;
	public bool stoppedJumping;


	private Rigidbody2D myRigidbody;
	//private Collider2D myCollider;
	private Animator myAnimator;
	private float jumpTimeCounter;
	private float speedMilestoneCount;
	private float moveSpeedStore;
	private float speedMileStoneCountStore;
	private float speedIncreaseMilestoneStore;
	private bool canDoubleJump;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		//myCollider = GetComponent<Collider2D>();
		myAnimator = GetComponent<Animator>();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedIncreaseMilestone;
		moveSpeedStore = moveSpeed;
		speedMileStoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		stoppedJumping = true;
		canDoubleJump = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Se il collider del player sta toccando il collider del Ground
		//grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround); 

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		//Il player va sempre più veloce quando percorre una certa distanza
		if(transform.position.x > speedMilestoneCount){
			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		}

		myRigidbody.velocity = new Vector2(moveSpeed,myRigidbody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
			
			if(grounded){ //Lo faccio saltare solo quando tocca il terreno
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;

			}

			if(!grounded && canDoubleJump){
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter = jumpTime;
				stoppedJumping = false;
				canDoubleJump = false;
			}
		}

		if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping){

			if(jumpTimeCounter > 0){
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
			
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if(grounded){
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		}


		//Do i valori all'Animator in modo che possa gestire le animazioni da come settato
		// nell'editor
		myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.tag == "killbox"){
			theGameManager.RestartGame();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMileStoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
		}
	}
}
