using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string playGameLevel;
    public string options;

    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }

    public void optionsMenu()
    {
        Application.LoadLevel(options);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
