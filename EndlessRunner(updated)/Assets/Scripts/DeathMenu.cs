using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;
    public Text bank;
    public Text grabbed;
    public Text total;
    public ScoreManager score;

    public int currentStash;
    public int collected;
    public int totalCoins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Bank"))
        {
            currentStash = PlayerPrefs.GetInt("Bank");
        }
        collected = score.coinCount;
        totalCoins = currentStash + collected;
        PlayerPrefs.SetInt("Bank", totalCoins);
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
        currentStash = PlayerPrefs.GetInt("Bank");
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
