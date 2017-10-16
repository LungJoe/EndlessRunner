using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class HighScoreManager : MonoBehaviour {

    public List<Text> playerScoreText;
    public string loginURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%20%22action%22:%22login%22},{%20%22login%22:%22bifrost_corhelm%22},{%20%22password%22:%22296aedae45078da5fea8a217986ec96d6234940c477bb0fcfc807ce58b9f737c%22},{%20%22app_code%22:%22r8CDypEHXwRNZ7xT%22},{%20%22session_type%22:%22session_key%22},{%20%22checksum%22:%22f3727e16f263407111ce2f46aef3c1bdab230743f59c4296bd429ba896271b22%22}]";
    public string getScoresURL;

    // Use this for initialization
    IEnumerator Start() {
        WWW loginRequest = new WWW(loginURL);
        yield return loginRequest;

        if (loginRequest.error != null)
        {
            Debug.Log("There was an error logging in: " + loginRequest.error);
        }
        else
        {
            // parses return JSON from login
            JsonData loginMsg = JsonMapper.ToObject(loginRequest.text);
            // concatenates mySQL call with session key from login request
            getScoresURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22SELECT%20*%20FROM%20HighScores%20%22},{%22session_key%22:%22" + loginMsg[3]["session_key"].ToString() + "%22}]";

            WWW pullFromDB = new WWW(getScoresURL);
            yield return pullFromDB;


            if (pullFromDB.error != null)
            {
                Debug.Log("There was an error getting the high score: " + pullFromDB.error);
            }
            else
            {
                JsonData pullMsg = JsonMapper.ToObject(pullFromDB.text);
                for (int i = 0; i < 17; i++)
                {
                    playerScoreText[i].text = "           " + pullMsg[1]["message"][i][1]["PlayerName"].ToString() + "     " + pullMsg[1]["message"][i][3]["MapName"].ToString() + pullMsg[1]["message"][i][2]["Score"].ToString();
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
    }
}