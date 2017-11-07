using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float attackJumpForce;
    public float fallForce;
    protected float moveSpeedStore;
    protected float speedMilestoneCountStore;
    protected float speedIncreaseMilstoneStore;


    public float speedMultiplier;
    public float speedIncreaseMilestone;
    public float speedCap;
    protected float speedMilestoneCount;

    public float jumpTime;
    private float jumpTimeCounter;

    private float startTime = 0f;
    public float holdTime = 1.0f;

    private Rigidbody2D myRigidbody;

    protected bool stoppedJumping;
    protected bool canDoubleJump;

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
    public bool isCRowdy;

    //private Collider2D myCollider
    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilstoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
        speedCap = 200;
    }

    // Update is called once per frame
    void Update()
    {

        //grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround); //if collider touches another, grounded = true

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            if (moveSpeed <= speedCap)
            {
                speedMilestoneCount += speedIncreaseMilestone;
                speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
                moveSpeed = moveSpeed * speedMultiplier;
            }
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isSliding && !isAttacking)
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
        
        if (Input.GetKeyDown(KeyCode.A) && canDoubleJump && !grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, attackJumpForce);
            isAttacking = true;
            canDoubleJump = false;
        }
        

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
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


        //Attack Functionality
       
        if (Input.GetKey(KeyCode.A) && grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, attackJumpForce);
            isAttacking = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isAttacking = false;
            canDoubleJump = false;
            startTime = 0f;
        }
        

        //Alternate Attack (not as good as above but wont keep attacking if button held
        /*
        if (Input.GetKeyDown(KeyCode.A) )
        {
            startTime = Time.time;
            holdTime = .2f;
        }
        if (Input.GetKey(KeyCode.A) && grounded || Input.GetKey(KeyCode.A) && !grounded && canDoubleJump)
        {
            if (startTime + holdTime <= Time.time)
            {
                isAttacking = false;
            }
            else
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, attackJumpForce);
                isAttacking = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isAttacking = false;
            startTime = 0f;
            canDoubleJump = false;
        }
        */

        //Slide Functionality
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(1)))
        {
            startTime = Time.time;
            holdTime = 1f;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetMouseButton(1)))
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
        else if ((Input.GetKeyUp(KeyCode.S) || Input.GetMouseButtonUp(1)))
        {
            isSliding = false;
            startTime = 0f;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetMouseButton(1)) && !grounded)
        {
            if (startTime + holdTime <= Time.time)
            {
                isSliding = false;
            }
            else
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, fallForce);
                isSliding = true;
            }
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(1)) && !grounded)
        {

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
        myAnimator.SetBool("CRowdy", isCRowdy);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //tagging an object -> easy way to detect what an object is(under object name in spector)
        if ((other.gameObject.tag == "killbox" && (!invincible || !(transform.position.y > -4))) || (other.gameObject.tag == "attackBox" && !isAttacking))
        {
           
            theGameManager.RestartGame();
            ResetValues();
        }
        if (other.gameObject.tag == "killbox" && invincible)
        {
            other.gameObject.SetActive(false);
            scorer.AddScore(200);
        }
        if (other.gameObject.tag == "attackBox" && isAttacking)
        {
            other.gameObject.SetActive(false);
        }
    }


    public void ResetValues()
    {
        
        isSliding = false;
        isAttacking = false;
        stoppedJumping = false;
        canDoubleJump = false;
        moveSpeed = moveSpeedStore;
        speedMilestoneCount = speedMilestoneCountStore;
        speedIncreaseMilestone = speedIncreaseMilstoneStore;
        deathSound.Play();
    }
}