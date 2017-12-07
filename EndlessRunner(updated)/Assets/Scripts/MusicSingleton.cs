using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Exception = System.Exception;

public class MusicSingleton : MonoBehaviour {
    private AudioSource _audioSource;
    private float gameVol;
	private int started;


    private void Start()
    {
			PlayerPrefs.GetFloat ("musicVol", 0.25F);
			PlayerPrefs.GetInt ("textureIndex", 0);
			gameVol = PlayerPrefs.GetFloat ("musicVol");
			PlayerPrefs.GetInt ("textureIndex");

			DontDestroyOnLoad (transform.gameObject);
			_audioSource = GetComponent<AudioSource> ();
			_audioSource.volume = gameVol;
			PlayerPrefs.SetInt ("started", 1);
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
