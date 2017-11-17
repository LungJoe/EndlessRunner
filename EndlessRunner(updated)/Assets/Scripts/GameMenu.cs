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
    public PlayerController thePlayer;
    public Button[] levels;
    public Button[] skins;
    public Text bankText;
    private void Start()
    {

        for(int i = 1; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }
        for(int i = 1; i < skins.Length; i++)
        {
            skins[i].interactable = false;
        }
        bankText.text = "Coins - " + PlayerPrefs.GetInt("Bank", 0);
        if (PlayerPrefs.GetInt("Bank") >= 100 && levels[1].interactable == false)
        {
            levels[1].interactable = true;
            setObjectsFalseByTag("Stage2");
        }
        if (PlayerPrefs.GetInt("Bank") >= 200 && levels[2].interactable == false)
        {
            levels[2].interactable = true;
            setObjectsFalseByTag("Stage3");
        }

        if (PlayerPrefs.GetInt("Bank") >= 50 && skins[1].interactable == false)
        {
            skins[1].interactable = true;
            if (GameObject.FindGameObjectWithTag("Skin2") != null)
                GameObject.FindGameObjectWithTag("Skin2").GetComponent<Image>().enabled = false;
        }
        if (PlayerPrefs.GetInt("Bank") >= 150 && skins[2].interactable == false)
        {
            skins[2].interactable = true;
            if (GameObject.FindGameObjectWithTag("Skin3") != null)
                GameObject.FindGameObjectWithTag("Skin3").GetComponent<Image>().enabled = false;
        }
    }

    private void Update()
    {

    }
    public void PlayStage1()
    {
        if (GameObject.FindGameObjectWithTag("Music") != null)
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicSingleton>().StopMusic();
        SceneManager.LoadScene(playEndless);
    }
    public void PlayStage2()
    {
        if (GameObject.FindGameObjectWithTag("Music") != null)
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicSingleton>().StopMusic();
        SceneManager.LoadScene(playEndless2);
    }

    public void PlayStage3()
    {
        if (GameObject.FindGameObjectWithTag("Music") != null)
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicSingleton>().StopMusic();
        SceneManager.LoadScene(playEndless3);
    }

    public void PickSkins1()
    {
        PlayerPrefs.SetInt("PlayerSkin", 1);
    }

    public void PickSkins2()
    {
        PlayerPrefs.SetInt("PlayerSkin", 2);

    }

    public void PickSkins3()
    {
        PlayerPrefs.SetInt("PlayerSkin", 3);

    }
    public void ViewScoreboard()
    {
        SceneManager.LoadScene(ScoreBoard);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

	public void setObjectsFalseByTag(string tag){
		GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag (tag);

		foreach(GameObject go in gameObjectArray)
		{
			go.SetActive (false);
		}
	}
}