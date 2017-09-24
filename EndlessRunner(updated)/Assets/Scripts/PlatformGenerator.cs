using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    
    
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;
    
    private float platformWidth;
    
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    
    
    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObejctPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;
    
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    public float randomSpikeThreshold;
    public ObejctPooler spikePool;

    public float powerupHeight;
    public ObejctPooler powerupPool;
    public float powerupThreshold;
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

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        currentPlatformSpaceChance = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            
            if (Random.Range(0f,100f) < powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();
                newPowerup.transform.position = transform.position + new Vector3((distanceBetween / 2f) + 4.3f, Random.Range((powerupHeight/2),powerupHeight), 0f);
                newPowerup.SetActive(true);
            }
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


            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 4.3f, transform.position.y + 1f, transform.position.z));
            }
            
            
            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                float spikeXPosition = Random.Range((-platformWidths[platformSelector]+4.3f) / 2f + 3f, (platformWidths[platformSelector]+4.3f) / 2f + 1.7f);


                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);


                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }
            
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) , transform.position.y, transform.position.z);

        }
	}

    private bool DoWeAddSpace(){
        int randomValueToDetermine = Random.Range(0, 100);
        if(randomValueToDetermine <= currentPlatformSpaceChance){
            currentPlatformSpaceChance = 0;
            if (DoWeChangeHeight())
            {
                heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            }
            else
            {
                heightChange = transform.position.y;
            }
            heightCheck();
            return true;
        }
        currentPlatformSpaceChance += 5;
        return false;
    }

    private bool DoWeChangeHeight(){
        int randomValueToDetermine = Random.Range(0, 100);
        if (randomValueToDetermine <= currentPlatformHeightChance)
        {
            currentPlatformHeightChance = 0;
            return true;
        }
        currentPlatformHeightChance += 4;
        return false;
    }

    private void heightCheck(){
        if (heightChange > maxHeight){
            heightChange = maxHeight;
        }
        else if (heightChange < minHeight){
            heightChange = minHeight;
        }
    }
}
