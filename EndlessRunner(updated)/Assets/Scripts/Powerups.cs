using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour {

    public bool doublePoints;
    public bool safeMode;
    public bool invincible;
    public float powerupLength;
    private int sprite;
    private PowerupManager thePowerupManager;

    public Sprite[] powerupSprites;
	// Use this for initialization
	void Start () {
        thePowerupManager = FindObjectOfType<PowerupManager>();
	}

    void Awake()
    {
        int powerupSelector = Random.Range(0, 100);
        if(powerupSelector > 83)
        {
            safeMode = true;
            sprite = 1;
        }
        else if(powerupSelector > 53)
        {
            invincible = true;
            sprite = 2;
        }
        else
        {
            doublePoints = true;
            sprite = 0;
        }
        GetComponent<SpriteRenderer>().sprite = powerupSprites[sprite];

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            thePowerupManager.ActivatePowerup(doublePoints, safeMode, invincible, 180f);
        }
        gameObject.SetActive(false);
    }
}
