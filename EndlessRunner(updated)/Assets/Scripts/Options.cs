using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    public string mainMenu;
    public Dropdown textureQuality;
    public Slider musicVolumeSlider;
    public AudioSource music;
    public Button applyButton;

    void OnEnable()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        textureQuality.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        LoadSettings();
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = textureQuality.value;
    }

    public void OnMusicVolumeChange()
    {
        music.volume = musicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("textureIndex", textureQuality.value);
        PlayerPrefs.SetFloat("musicVol", music.volume);
        SceneManager.LoadScene(mainMenu);

    }

    public void LoadSettings()
    {
        textureQuality.value = PlayerPrefs.GetInt("textureIndex", 0);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVol", 0.25F);
    }
}
