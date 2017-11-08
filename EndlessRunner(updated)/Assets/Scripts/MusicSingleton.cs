using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Exception = System.Exception;

public class MusicSingleton : MonoBehaviour {
    private AudioSource _audioSource;
    private GameSettings gameSettings;
    private float gameVol;

    private void Awake()
    {
		gameSettings = new GameSettings ();
		try{
        	gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.dataPath + "/gamesettings.json"));
		} catch(Exception e){
			gameSettings.fullScreen = false;
			gameSettings.resolutionIndex = 0;
			gameSettings.textureQuality = 0;
			gameSettings.musicVolume = 0.25F;
			string jsonData = JsonUtility.ToJson(gameSettings, true);
			File.WriteAllText(Application.dataPath + "/gamesettings.json", jsonData);
		}

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
