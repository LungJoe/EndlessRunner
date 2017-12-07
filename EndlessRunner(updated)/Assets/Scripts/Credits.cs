using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour {
    public string GameMenu;
    public Animator animation;
	// Use this for initialization
	void Start () {
        animation.GetComponent<Animator>().Play("CreditsAnimation");
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void GoBackToGameMenu()
    {
        SceneManager.LoadScene(GameMenu);
    }


}
