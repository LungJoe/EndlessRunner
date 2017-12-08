using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {
    
    public string MainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }


}
