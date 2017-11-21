using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Exception = System.Exception;

public class MusicSingleton : MonoBehaviour {
    private AudioSource _audioSource;
    private float gameVol;

    private void Awake()
    {
        gameVol = PlayerPrefs.GetFloat("musicVol", 0.25F);
        PlayerPrefs.GetInt("textureIndex", 0);

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
