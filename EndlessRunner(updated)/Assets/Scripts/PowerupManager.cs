using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
    public bool doublePoints;
    public bool safeMode;
    public bool noDeathMode;

    public PlayerController player;
    public bool powerupActive;

    private float powerupLengthCounter;
    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;

    private float normalPointsPerSecond;
    private float spikeRate;

    private PlatformDestroyer[] spikeList;
    private GameManager theGameManager;
    // Use this for initialization
	void Start () {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (powerupActive == true)
        {
            powerupLengthCounter--;

            if (theGameManager.powerupReset)
            {
                powerupLengthCounter = 0;
                theGameManager.powerupReset = false;
            }

            if (doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;
                theScoreManager.shouldDouble = true;
            }

            if (safeMode)
            {
                thePlatformGenerator.randomSpikeThreshold = 0;
            }

            if (noDeathMode)
            {
                player.invincible = true;
            }
            if (powerupLengthCounter <= 0)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond;
                theScoreManager.shouldDouble = false;
                thePlatformGenerator.randomSpikeThreshold = spikeRate;
                player.invincible = false;
                doublePoints = false;
                safeMode = false;
                noDeathMode = false;
                powerupActive = false;
            }
        }
	}

    public void ActivatePowerup(bool points, bool safe, bool noDeath, float time)
    {
        doublePoints = points;
        safeMode = safe;
        noDeathMode = noDeath;
        powerupLengthCounter = time;

        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        spikeRate = thePlatformGenerator.randomSpikeThreshold;
        spikeList = FindObjectsOfType<PlatformDestroyer>();
        if (safeMode)
        {
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }
        powerupActive = true;
    }

}
