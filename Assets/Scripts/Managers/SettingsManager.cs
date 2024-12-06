using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown fpsDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    private bool isGamePaused = false;
    private Resolution[] availableResolutions;

    private void Start()
    {
        if(settingsButton) settingsButton.onClick.AddListener(ToggleSettings);
        closeButton.onClick.AddListener(CloseSettings);
        exitGameButton.onClick.AddListener(ExitGame);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);
        
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var resolutionOptions = new List<string>();
        foreach (var resolution in availableResolutions)
        {
            resolutionOptions.Add($"{resolution.width}x{resolution.height}");
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = GetCurrentResolutionIndex();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        
        fpsDropdown.ClearOptions();
        var fpsOptions = new List<string> { "Unlimited", "30", "60", "120" };
        fpsDropdown.AddOptions(fpsOptions);
        fpsDropdown.value = 1;
        fpsDropdown.onValueChanged.AddListener(SetFPSLimit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSettings();
        }
    }

    private void ToggleSettings()
    {
        if (settingsPanel == null) return;

        var isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);

        isGamePaused = !isActive;
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    private void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Time.timeScale = 1;
        isGamePaused = false;
    }
    
    private void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void SetEffectsVolume(float value)
    {
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }
    
    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i].width == Screen.width && availableResolutions[i].height == Screen.height)
                return i;
        }
        return 0;
    }
    
    private void SetResolution(int index)
    {
        var resolution = availableResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SetFPSLimit(int index)
    {
        switch (index)
        {
            case 0:
                Application.targetFrameRate = -1;
                break;
            case 1:
                Application.targetFrameRate = 30;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
        }
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}