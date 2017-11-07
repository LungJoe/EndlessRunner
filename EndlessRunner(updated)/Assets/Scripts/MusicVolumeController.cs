using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicVolumeController : MonoBehaviour {
    private AudioSource _audioSource;
    private GameSettings gameSettings;
    private float gameVol;
    // Use this for initialization
    void Start () {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.dataPath + "/gamesettings.json"));
        gameVol = gameSettings.musicVolume;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = gameVol;
    }
}
