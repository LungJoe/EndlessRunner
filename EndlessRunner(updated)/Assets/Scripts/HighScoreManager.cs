using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

    public Text firstPlayerScoreText;
    public string loginURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%20%22action%22:%22login%22},{%20%22login%22:%22bifrost_corhelm%22},{%20%22password%22:%22296aedae45078da5fea8a217986ec96d6234940c477bb0fcfc807ce58b9f737c%22},{%20%22app_code%22:%22r8CDypEHXwRNZ7xT%22},{%20%22session_type%22:%22session_key%22},{%20%22checksum%22:%22f3727e16f263407111ce2f46aef3c1bdab230743f59c4296bd429ba896271b22%22}]";
    public string getScoresURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22SELECT%20*%20FROM%20HighScores%20ORDER%20BY%20HighScores.Score%20DESC%20%22},{%22session_key%22:%22245c493f48bd5f2f609aa201480e7a0b87df4c95f30d9c1f313d03c54185ba8b%22}]";
    public WWW hs_get;

	// Use this for initialization
	IEnumerator Start () {
        WWW login = new WWW(loginURL);
        yield return login;
        hs_get = new WWW(getScoresURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            firstPlayerScoreText.text = hs_get.text;
        }
	}
	
	// Update is called once per frame
	void Update () {
    }
}
