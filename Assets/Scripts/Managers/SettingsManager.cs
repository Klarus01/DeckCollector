using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown fpsDropdown;
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private MusicManager musicManager;
    
    private bool isGamePaused;
    private Resolution[] availableResolutions;

    private void Start()
    {
        InitializeButtons();
        InitializeSliders();
        InitializeDropdowns();
        InitializeToggle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSettings();
        }
    }

    private void InitializeButtons()
    {
        if (settingsButton) settingsButton.onClick.AddListener(ToggleSettings);
        closeButton.onClick.AddListener(CloseSettings);
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            mainMenuButton.gameObject.SetActive(false);
        }
        else
        {
            mainMenuButton.gameObject.SetActive(true);
            mainMenuButton.onClick.AddListener(BackToMainMenu);
        }
    }

    private void InitializeSliders()
    {
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);

        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);
    }

    private void InitializeDropdowns()
    {
        InitializeResolutionDropdown();
        InitializeFPSDropdown();
        _ = InitializeLanguageDropdown();
    }

    private void InitializeResolutionDropdown()
    {
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
    }

    private void InitializeFPSDropdown()
    {
        var fpsOptions = new List<string> { "Unlimited", "30", "60", "120" };
        fpsDropdown.ClearOptions();
        fpsDropdown.AddOptions(fpsOptions);
        fpsDropdown.value = 1;
        fpsDropdown.onValueChanged.AddListener(SetFPSLimit);
    }
    
    private async Task InitializeLanguageDropdown()
    {
        await LocalizationSettings.InitializationOperation.Task;

        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        languageDropdown.ClearOptions();

        foreach (var locale in availableLocales)
        {
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
        }

        var savedLanguageIndex = PlayerPrefs.GetInt("SelectedLanguage", 0);
        languageDropdown.value = Mathf.Clamp(savedLanguageIndex, 0, availableLocales.Count - 1);

        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }



    private void InitializeToggle()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void ToggleSettings()
    {
        if (settingsPanel == null) return;

        bool isActive = settingsPanel.activeSelf;
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
        musicManager.SetMusicVolume(value);
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
        Resolution resolution = availableResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SetFPSLimit(int index)
    {
        int[] fpsValues = { -1, 30, 60, 120 };
        Application.targetFrameRate = fpsValues[index];
    }

    private void ChangeLanguage(int index)
    {
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        LocalizationSettings.SelectedLocale = availableLocales[index];

        PlayerPrefs.SetInt("SelectedLanguage", index);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}