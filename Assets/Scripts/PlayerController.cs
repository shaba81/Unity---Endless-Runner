using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public bool grounded;
	public LayerMask whatIsGround; //selezione del layer che ho inserito su Unity

	private Rigidbody2D myRigidbody;
	private Collider2D myCollider;
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<Collider2D>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//Se il collider del player sta toccando il collider del Ground
		grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround); 

		myRigidbody.velocity = new Vector2(moveSpeed,myRigidbody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
			
			if(grounded) //Lo faccio saltare solo quando tocca il terreno
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
		
		}

		//Do i valori all'Animator in modo che possa gestire le animazioni da come settato
		// nell'editor
		myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool("Grounded", grounded);
	}
}
