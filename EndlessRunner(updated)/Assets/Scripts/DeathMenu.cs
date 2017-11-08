using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;
    public Text bank;
    public Text grabbed;
    public Text total;
    public ScoreManager score;
    public string loginURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%20%22action%22:%22login%22},{%20%22login%22:%22bifrost_corhelm%22},{%20%22password%22:%22296aedae45078da5fea8a217986ec96d6234940c477bb0fcfc807ce58b9f737c%22},{%20%22app_code%22:%22r8CDypEHXwRNZ7xT%22},{%20%22session_type%22:%22session_key%22},{%20%22checksum%22:%22f3727e16f263407111ce2f46aef3c1bdab230743f59c4296bd429ba896271b22%22}]";
    public string postScoreURL;
    public string sceneName;
    public Button postHighScoreButton;
    public InputField highScoreName;

    public int currentStash;
    public int collected;
    public int totalCoins;

    private void Start()
    {
        currentStash = PlayerPrefs.GetInt("Bank", 0);
        collected = score.coinCount;
        totalCoins = currentStash + collected;
        PlayerPrefs.SetInt("Bank", totalCoins);
    }

    public void PostHighScore()
    {
        // tests to see if name was entered, doesn't post if no name
        if (highScoreName.text.Length >= 2)
            StartCoroutine(StartPostHighScore());
        else
        {
            postHighScoreButton.GetComponentInChildren<Text>().color = Color.red;
            postHighScoreButton.GetComponentInChildren<Text>().text = "Enter a Valid Name";
        }
    }

    private IEnumerator StartPostHighScore()
    {
        // finds current scene name
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "Endless":
                sceneName = "Desert";
                break;
            case "Endless 2":
                sceneName = "Cave";
                break;
            case "Endless 3":
                sceneName = "UTSA";
                break;
        }

        // user input name for high score post
        string name = highScoreName.text;

        // makes login json call to server
        WWW loginRequest = new WWW(loginURL);
        yield return loginRequest;

        if (loginRequest.error != null)
        {
            Debug.Log("There was an error logging in: " + loginRequest.error);
        }
        else
        {
            // parses return JSON from login
            JsonData loginMsg = JsonMapper.ToObject(loginRequest.text);
            // concatenates mySQL push call with session key from login request
            postScoreURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22INSERT%20INTO%20HighScores(id,%20PlayerName,%20Score,%20MapName)%20VALUES%20(NULL,%27" + name + "%27," + Mathf.Round(score.scoreCount).ToString() + ",%27" + sceneName + "%27)%22},{%22session_key%22:%22" + loginMsg[3]["session_key"].ToString() + "%22}]";

            // makes push call to server
            WWW postScore = new WWW(postScoreURL);
            yield return postScore;

            if (postScore.error != null)
            {
                Debug.Log("There was an error posting the high score: " + postScore.error);
            }
            else
            {
                postHighScoreButton.GetComponentInChildren<Text>().color = Color.black;
                postHighScoreButton.GetComponentInChildren<Text>().text = "Posted!";
                postHighScoreButton.interactable = false;
            }
        }
    }

    private void Update()
    {
        collected = score.coinCount;
        bank.text = "Stash:\t" + currentStash;
        grabbed.text = "Collected:\t" + collected;
        total.text = "Total:\t" + (currentStash + collected);
        Debug.Log(collected);
    }

    public void setText()
    {
        currentStash = PlayerPrefs.GetInt("Bank", 0);
        Debug.Log(collected);
        totalCoins = currentStash + collected;
        collected = 0;
        PlayerPrefs.SetInt("Bank", totalCoins);
    }
    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }
}
