using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicVolumeController : MonoBehaviour {
    private AudioSource _audioSource;
    private float gameVol;

    // Use this for initialization
    void Start () {
        gameVol = PlayerPrefs.GetFloat("musicVol");
        print(gameVol);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = gameVol;
    }
}
