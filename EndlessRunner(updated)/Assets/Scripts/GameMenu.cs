using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public string playEndless;
    public string playEndless2;
    public string playEndless3;
    public string MainMenu;
    public string ScoreBoard;
    public string gameMenu;

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