﻿using System.Collections;
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

    private float startTime = 0f;
    public float holdTime = 1.5f;

    private Rigidbody2D myRigidbody;

    private bool stoppedJumping;
    private bool canDoubleJump;
    
    public bool grounded;

    public LayerMask WhatIsGround;//selection of the layers available
    public Transform groundCheck;
    public float groundCheckRadius;
    private Animator myAnimator;

    public GameManager theGameManager;
    public ScoreManager scorer;
    public AudioSource jumpSound;
    public AudioSource deathSound;

    public bool isAttacking;
    public bool isSliding;
    public bool invincible = false;

    //private Collider2D myCollider
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
       // myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
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
            startTime = Time.time;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (startTime + holdTime <= Time.time)
            {
                    isSliding = false;
            }
            else
            {
                    isSliding = true;
            }
            
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            isSliding = false;
            startTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            startTime = Time.time;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (startTime + holdTime <= Time.time)
            {
                isAttacking = false;
            }
            else
            {
                isAttacking  = true;
            }

        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            isAttacking = false;
            startTime = 0f;
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
        if (other.gameObject.tag == "killbox" && (!invincible || !(transform.position.y > -4)))
        {
            theGameManager.RestartGame();
            isSliding = false;
            isAttacking = false;
            stoppedJumping = false;
            canDoubleJump = false;
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilstoneStore;
            deathSound.Play();

        }
        if(other.gameObject.tag == "killbox" && invincible)
        {
            other.gameObject.SetActive(false);
            scorer.AddScore(200);
        }

    }

}
