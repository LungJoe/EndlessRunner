using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGenerator : MonoBehaviour {
    
    
    public GameObject thePlatform;
    public Transform generationPoint;
    public PlayerController thePlayer;
    public float distanceBetween;
    private int playerModifier;
    private int prevPlayerMod;
    private float platformWidth;
    
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float distanceBetweenMinCurrent;
    private float distanceBetweenMaxCurrent;


    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObejctPooler[] theObjectPools;
    
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    private float spikeDifficulty = 10;
    private float slideDifficulty = 10;
    private float attackDifficulty = 10;
    public float randomSpikeThreshold;
    public float randomSlideObstacleThreshold;
    public float randomAttackObstacleThreshold;
    public ObejctPooler spikePool;
    public ObejctPooler slideObstaclePool;
    public ObejctPooler attackObstaclePool;

    public float powerupHeight;
    public ObejctPooler powerupPool;
    private float powerupThreshold;
    private float powerluck = 10;
    private int currentPlatformSpaceChance;
    private int currentPlatformHeightChance;

    //public ObejctPooler theObjectPool;



	// Use this for initialization
	void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;



        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        currentPlatformSpaceChance = 0;
        playerModifier = (int)(thePlayer.moveSpeed / 10);
        prevPlayerMod = playerModifier;
        distanceBetweenMinCurrent = distanceBetweenMin * playerModifier;
        distanceBetweenMaxCurrent = distanceBetweenMax * playerModifier;
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        playerModifier = (int)(thePlayer.moveSpeed / 10);
        if(prevPlayerMod != playerModifier)
        {
            prevPlayerMod = playerModifier;
            distanceBetweenMinCurrent = distanceBetweenMin * playerModifier;
            distanceBetweenMaxCurrent = distanceBetweenMax * playerModifier;
        }
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMinCurrent, distanceBetweenMaxCurrent);

            platformSelector = Random.Range(0, theObjectPools.Length);

            
            if (DoWeAddSpace()){
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);
            }
            else{
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
            }

            //Instantiate(/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);


            
            

            if (generateSlideObstacle() && ((currentScene.name == "Endless 2") || (currentScene.name == "Endless 3")))
            {
                GameObject newSlideObstacle = slideObstaclePool.GetPooledObject();

                float slideObstacleXPosition = Random.Range((-platformWidths[platformSelector]) / 2f + 3f, (platformWidths[platformSelector]) / 2f - 3f);

                Vector3 slideObstaclePosition = new Vector3(slideObstacleXPosition, 5f, 0f);

                newSlideObstacle.transform.position = transform.position + slideObstaclePosition;
                newSlideObstacle.transform.rotation = transform.rotation;
                newSlideObstacle.SetActive(true);
            }
            else if (generateAttackObstacle() && (currentScene.name == "Endless 3"))
            {
                GameObject newAttackObstacle = attackObstaclePool.GetPooledObject();

                float attackObstacleXPosition = Random.Range((-platformWidths[platformSelector]) / 2f + 3f, (platformWidths[platformSelector]) / 2f - 3f);

                Vector3 attackObstaclePosition = new Vector3(attackObstacleXPosition, 1f, 0f);

                newAttackObstacle.transform.position = transform.position + attackObstaclePosition;
                newAttackObstacle.transform.rotation = transform.rotation;
                newAttackObstacle.SetActive(true);
            }
            else if (generateSpike())
            {
                if (!spikePool.HasPooledObject())
                {
                    GameObject newSpike = spikePool.GetPooledObject();
 
                    float spikeXPosition = Random.Range((-platformWidths[platformSelector]) / 2f + 1f, (platformWidths[platformSelector]) / 2f - 1f);


                    Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);


                    newSpike.transform.position = transform.position + spikePosition;
                    newSpike.transform.rotation = transform.rotation;
                    newSpike.SetActive(true);
                }
            }
            else if (generatePowerup())
            {
                GameObject newPowerup = powerupPool.GetPooledObject();
                newPowerup.transform.position = transform.position + new Vector3((distanceBetween / 2f), Random.Range((powerupHeight / 2), powerupHeight), 0f);
                newPowerup.SetActive(true);
            }
            else if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) , transform.position.y, transform.position.z);

        }
	}

    private bool DoWeAddSpace(){
        int randomValueToDetermine = Random.Range(0, 100);
        if(randomValueToDetermine <= currentPlatformSpaceChance){
            currentPlatformSpaceChance = 0;
            return true;
        }
        currentPlatformSpaceChance += 5;
        return false;
    }

    private bool generateSpike()
    {
        int randomValueToDetermine = Random.Range(0, 100);
        if (randomValueToDetermine <= randomSpikeThreshold)
        {
            randomSpikeThreshold = spikeDifficulty*(playerModifier-1);
            return true;
        }
        randomSpikeThreshold += 2;
        return false;
    }

    private bool generateSlideObstacle()
    {
        int randomValueToDetermine = Random.Range(0, 100);
        if (randomValueToDetermine <= randomSlideObstacleThreshold)
        {
            randomSlideObstacleThreshold = slideDifficulty * (playerModifier - 1);
            return true;
        }
        randomSlideObstacleThreshold += 2;
        return false;

    }

    private bool generateAttackObstacle()
    {
        int randomValueToDetermine = Random.Range(0, 100);
        if (randomValueToDetermine <= randomAttackObstacleThreshold)
        {
            randomAttackObstacleThreshold = attackDifficulty * (playerModifier - 1);
            return true;
        }
        randomAttackObstacleThreshold += 2;
        return false;

    }


    private bool generatePowerup()
    {
        int randomValueToDetermine = Random.Range(0, 100);
        if (randomValueToDetermine <= powerupThreshold)
        {
            powerupThreshold = powerluck/(playerModifier);
            return true;
        }
        powerupThreshold += 2;
        return false;
    }
}
