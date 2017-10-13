using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Application.LoadLevel(playEndless);
    }
    public void PlayStage2()
    {
        Application.LoadLevel(playEndless2);
    }

    public void PlayStage3()
    {
        Application.LoadLevel(playEndless3);
    }

    public void ViewScoreboard()
    {
        Application.LoadLevel(ScoreBoard);
    }

    public void GoBackToMainMenu()
    {
        Application.LoadLevel(MainMenu);
    }

    public void GoToGameMenu()
    {
        Application.LoadLevel(gameMenu);
    }

}