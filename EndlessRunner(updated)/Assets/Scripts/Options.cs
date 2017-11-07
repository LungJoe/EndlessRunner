using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

    public string mainMenu;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropDown;
    public Dropdown textureQuality;
    public Slider musicVolumeSlider;
    public AudioSource music;
    public Button applyButton;

    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQuality.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropDown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();
    }

    public void OnFullScreenToggle()
    {

        gameSettings.fullScreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropDown.value].width, resolutions[resolutionDropDown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropDown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQuality.value;
        
    }

    public void OnMusicVolumeChange()
    {
        music.volume = gameSettings.musicVolume = musicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.dataPath + "/gamesettings.json", jsonData);
        SceneManager.LoadScene(mainMenu);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.dataPath + "/gamesettings.json"));
        fullscreenToggle.isOn = gameSettings.fullScreen;
        textureQuality.value = gameSettings.textureQuality;
        resolutionDropDown.value = gameSettings.resolutionIndex;
        musicVolumeSlider.value = gameSettings.musicVolume;
        Screen.fullScreen = gameSettings.fullScreen;

        resolutionDropDown.RefreshShownValue();
    }
}
