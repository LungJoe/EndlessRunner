using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


    public Text scoreText;
    public Text highScoreText;
    public Text coinText;

    public float scoreCount;
    public float highScoreCount;
    private float scoreCountStore;
    public int coinCount;

    public float pointsPerSecond;
    private float pointsPerSecondStore;
    private float timer;

    public bool scoreIncreasing;

    public bool shouldDouble;

	// Use this for initialization
	void Start () {
        highScoreCount = PlayerPrefs.GetFloat("HighScore", 0);
        pointsPerSecondStore = pointsPerSecond;
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
        coinText.text = "Coins: " + coinCount;

	}

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)
        {
            pointsToAdd = pointsToAdd * 2;
        }
        scoreCount += pointsToAdd;
    }

    public void AddCoin()
    {
        coinCount++;
    }

    public void Reset()
    {
        scoreIncreasing = true;
        coinCount = 0;
        scoreCount = 0;
        pointsPerSecond = pointsPerSecondStore;
    }
}
