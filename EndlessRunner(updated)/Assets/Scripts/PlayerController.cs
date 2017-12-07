using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public GameObject resumeButton;
    public GameObject quitGameButton;
    public GameObject restartButton;

    public bool isAttacking;
    public bool isSliding;
    public bool invincible = false;
    public bool isCRowdy;
    public bool isARowdy;
    public bool isRRowdy;

    public ButtonImageSwitch skinButton;

    //private Collider2D myCollider
    // Use this for initialization
    void Start()
    {
        
        isARowdy = false;
        isRRowdy = true;
        isCRowdy = false;
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
        switch(PlayerPrefs.GetInt("PlayerSkin", 1))
        {
            case 1:
                isARowdy = false;
                isRRowdy = true;
                isCRowdy = false;
                break;
            case 2:
                isARowdy = true;
                isRRowdy = false;
                isCRowdy = false;
                break;
            case 3:
                isARowdy = false;
                isRRowdy = false;
                isCRowdy = true;
                break;
        }
 
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

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isSliding && !isAttacking && !GUIClicked())
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
        

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping && !GUIClicked())
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
				jumpSound.volume = PlayerPrefs.GetFloat("musicVol");
                jumpSound.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;

        }


        //Attack functionality
        if ( Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !isSliding)
        {
            startTime = Time.time;
            holdTime = .2f;
        }
        if ( Input.GetKey(KeyCode.A) && grounded && !Input.GetKeyDown(KeyCode.S) && !isSliding)
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
        else if (Input.GetKeyUp(KeyCode.A) && !Input.GetKeyDown(KeyCode.S))
        {
            isAttacking = false;
            startTime = 0f;
            canDoubleJump = false;
        }
      

        //Slide Functionality
        if ( (Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(1)) && !isAttacking)
        {
            startTime = Time.time;
            holdTime = 1f;
        }
        if ( (Input.GetKey(KeyCode.S) || Input.GetMouseButton(1)) && !Input.GetKeyDown(KeyCode.A) && !isAttacking)
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

        if ((Input.GetKey(KeyCode.S) || Input.GetMouseButton(1)) && !grounded && !isAttacking)
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
        myAnimator.SetBool("ARowdy", isARowdy);
        myAnimator.SetBool("RRowdy", isRRowdy);
    }

    bool GUIClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == GameObject.Find("PauseButton"))
                return true;
            if (EventSystem.current.currentSelectedGameObject == resumeButton)
                return true;
            if (EventSystem.current.currentSelectedGameObject == quitGameButton)
                return true;
            if (EventSystem.current.currentSelectedGameObject == restartButton)
                return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //tagging an object -> easy way to detect what an object is(under object name in spector)
        //if ((other.gameObject.tag == "killbox" && (!invincible || !(transform.position.y > -4))) || (other.gameObject.tag == "attackBox" && !isAttacking))

        if ((other.gameObject.tag == "Pit") || (other.gameObject.tag == "attackBox" && !isAttacking)  || (other.gameObject.tag == "killbox" && !invincible))
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
        SomeKeyIsDown = false;
        isSliding = false;
        isAttacking = false;
        stoppedJumping = false;
        canDoubleJump = false;
        moveSpeed = moveSpeedStore;
        speedMilestoneCount = speedMilestoneCountStore;
        speedIncreaseMilestone = speedIncreaseMilstoneStore;
		deathSound.volume = PlayerPrefs.GetFloat ("musicVol");
        deathSound.Play();
    }
}