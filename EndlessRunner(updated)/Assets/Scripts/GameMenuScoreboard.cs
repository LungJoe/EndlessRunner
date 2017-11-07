using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class GameMenuScoreboard : MonoBehaviour {

    public List<Text> nameText1;
    public List<Text> scoreText1;
    public List<Text> nameText2;
    public List<Text> scoreText2;
    public List<Text> nameText3;
    public List<Text> scoreText3;
    public string loginURL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%20%22action%22:%22login%22},{%20%22login%22:%22bifrost_corhelm%22},{%20%22password%22:%22296aedae45078da5fea8a217986ec96d6234940c477bb0fcfc807ce58b9f737c%22},{%20%22app_code%22:%22r8CDypEHXwRNZ7xT%22},{%20%22session_type%22:%22session_key%22},{%20%22checksum%22:%22f3727e16f263407111ce2f46aef3c1bdab230743f59c4296bd429ba896271b22%22}]";
    public string sessionKey;
    public string getScores1URL;
    public string getScores2URL;
    public string getScores3URL;

    IEnumerator Start()
    {
        // makes login json call to server
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
            sessionKey = loginMsg[3]["session_key"].ToString();

            StartCoroutine(GetStage1HighScores());
            StartCoroutine(GetStage2HighScores());
            StartCoroutine(GetStage3HighScores());
        }
    }

    public IEnumerator GetStage1HighScores()
    {
        // concatenates mySQL call with session key from login request
        getScores1URL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22SELECT%20*%20FROM%20HighScores%20WHERE%20MapName%20=%20%27Desert%27%20ORDER%20BY%20HighScores.Score%20DESC%20LIMIT%200,3%22},{%22session_key%22:%22" + sessionKey + "%22}]";

        // makes pull json call to server
        WWW pullFromDB1 = new WWW(getScores1URL);
        yield return pullFromDB1;


        if (pullFromDB1.error != null)
        {
            Debug.Log("There was an error getting the high score: " + pullFromDB1.error);
        }
        else
        {
            // parses return JSON from pull
            JsonData pullMsg = JsonMapper.ToObject(pullFromDB1.text);
            // sets each player's name, map, and score
            for (int i = 0; i < 3; i++)
            {
                nameText1[i].text = " " + pullMsg[1]["message"][i][1]["PlayerName"].ToString();
                scoreText1[i].text = pullMsg[1]["message"][i][2]["Score"].ToString();
            }
        }
    }

    public IEnumerator GetStage2HighScores()
    {
        // concatenates mySQL call with session key from login request
        getScores2URL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22SELECT%20*%20FROM%20HighScores%20WHERE%20MapName%20=%20%27Cave%27%20ORDER%20BY%20HighScores.Score%20DESC%20LIMIT%200,3%22},{%22session_key%22:%22" + sessionKey + "%22}]";

        // makes pull json call to server
        WWW pullFromDB2 = new WWW(getScores2URL);
        yield return pullFromDB2;


        if (pullFromDB2.error != null)
        {
            Debug.Log("There was an error getting the high score: " + pullFromDB2.error);
        }
        else
        {
            // parses return JSON from pull
            JsonData pullMsg = JsonMapper.ToObject(pullFromDB2.text);
            // sets each player's name, map, and score
            for (int i = 0; i < 3; i++)
            {
                nameText2[i].text = " " + pullMsg[1]["message"][i][1]["PlayerName"].ToString();
                scoreText2[i].text = pullMsg[1]["message"][i][2]["Score"].ToString();
            }
        }
    }

    public IEnumerator GetStage3HighScores()
    {
        // concatenates mySQL call with session key from login request
        getScores3URL = "https://easel1.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22query%22:%22SELECT%20*%20FROM%20HighScores%20WHERE%20MapName%20=%20%27UTSA%27%20ORDER%20BY%20HighScores.Score%20DESC%20LIMIT%200,3%22},{%22session_key%22:%22" + sessionKey + "%22}]";

        // makes pull json call to server
        WWW pullFromDB3 = new WWW(getScores3URL);
        yield return pullFromDB3;


        if (pullFromDB3.error != null)
        {
            Debug.Log("There was an error getting the high score: " + pullFromDB3.error);
        }
        else
        {
            // parses return JSON from pull
            JsonData pullMsg = JsonMapper.ToObject(pullFromDB3.text);
            // sets each player's name, map, and score
            for (int i = 0; i < 3; i++)
            {
                nameText3[i].text = " " + pullMsg[1]["message"][i][1]["PlayerName"].ToString();
                scoreText3[i].text = pullMsg[1]["message"][i][2]["Score"].ToString();
            }
        }
    }
}
