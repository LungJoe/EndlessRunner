using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public string playEndless;
    public string playEndless2;
    public string playEndless3;
    public string MainMenu;
    public string ScoreBoard;
    public string gameMenu;

    public Button[] levels;
    public Text bankText;
    private void Start()
    {
        for(int i = 1; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }

        bankText.text = "Coins - " + PlayerPrefs.GetInt("Bank", 0);
    }

    private void Update()
    {
        bankText.text = "Coins - " + PlayerPrefs.GetInt("Bank", 0);
        if(PlayerPrefs.GetInt("Bank") >= 1000 && levels[1].interactable == false)
        {
            levels[1].interactable = true;
        }
        if(PlayerPrefs.GetInt("Bank") >= 1500 && levels[2].interactable == false)
        {
            levels[2].interactable = true;
        }
    }
    public void PlayStage1()
    {
        SceneManager.LoadScene(playEndless);
    }
    public void PlayStage2()
    {
        SceneManager.LoadScene(playEndless2);
    }

    public void PlayStage3()
    {
        SceneManager.LoadScene(playEndless3);
    }

    public void ViewScoreboard()
    {
        SceneManager.LoadScene(ScoreBoard);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void GoToGameMenu()
    {
        SceneManager.LoadScene(gameMenu);
    }

}