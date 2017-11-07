﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicSingleton : MonoBehaviour {
    private AudioSource _audioSource;
    private GameSettings gameSettings;
    private float gameVol;

    private void Awake()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.dataPath + "/gamesettings.json"));

        gameVol = gameSettings.musicVolume;

        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = gameVol;
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
