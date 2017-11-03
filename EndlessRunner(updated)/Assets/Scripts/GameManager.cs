using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;
    public DeathMenu theDeathScreen;
    public Button postHighScoreButton;

    public GameObject pauseButton;
    public bool powerupReset;
	// Use this for initialization
	void Start () {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;
        theScoreManager = FindObjectOfType<ScoreManager>();
        pauseButton = GameObject.Find("PauseButton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        theDeathScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
       // thePlayer.speedMultiplier = 0;
      //  thePlayer.speedIncreaseMilestone = 0;

        //coroutine runs by itself, independently -> can add in time delays(reason why not in update)
        //StartCoroutine("RestartGameCo");    
    }

    public void Reset()
    {
        // reset post score button
        postHighScoreButton.GetComponentInChildren<Text>().text = "Post High Score";
        postHighScoreButton.interactable = true;
        
        theDeathScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        thePlayer.ResetValues();
        theScoreManager.Reset();
        powerupReset = true;
        theDeathScreen.setText();
    }


   /* public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }
    */
}
