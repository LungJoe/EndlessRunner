using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string playGameLevel;
    public string options;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicSingleton>().PlayMusic();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(options);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
