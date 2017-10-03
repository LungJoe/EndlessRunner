using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    private float moveSpeedStore;
    private float speedMilestoneCountStore;
    private float speedIncreaseMilstoneStore;


    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;

    public float jumpTime;
    private float jumpTimeCounter;

    public float slideTime;
    private float slideTimeCounter;

    private Rigidbody2D myRigidbody;

    private bool stoppedJumping;
    private bool canDoubleJump;
    
    public bool grounded;

    public LayerMask WhatIsGround;//selection of the layers available
    public Transform groundCheck;
    public float groundCheckRadius;
    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    public bool isAttacking;
    public bool isSliding;

    //private Collider2D myCollider
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
       // myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        slideTimeCounter = slideTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilstoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {

        //grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround); //if collider touches another, grounded = true

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)  && !isSliding && !isAttacking)
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;//add this if you want a holded second jump

                stoppedJumping = false;
                canDoubleJump = false;

            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping )
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                jumpSound.Play();
            }
        }
            
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (grounded)
            {
                isSliding = true;
                slideTimeCounter -= Time.deltaTime;
            }
            else
            {
                isSliding = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            isSliding = false;
            slideTimeCounter = slideTime;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if (grounded)
            {  
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            isAttacking = false;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetBool("Sliding", isSliding);
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("Attacking", isAttacking);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        //tagging an object -> easy way to detect what an object is(under object name in spector)
        if (other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilstoneStore;
            deathSound.Play();

        }

    }

}
