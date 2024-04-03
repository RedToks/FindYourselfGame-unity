using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private SaveLoad saveLoad;

    private GameSettings currentSettings;

    private Resolution[] resolutions;

    private void Start()
    {
        currentSettings = saveLoad.LoadSettings();
        if (currentSettings == null)
        {
            currentSettings = new GameSettings();
        }

        audioMixer.SetFloat("volume", currentSettings.volume);

        resolutions = Screen.resolutions;
        PopulateResolutionDropdown();
        SetDropdownValue(currentSettings.resolutionWidth, currentSettings.resolutionHeight);

        volumeSlider.value = currentSettings.volume;
        qualityDropdown.value = currentSettings.qualityLevel;
        fullScreenToggle.isOn = currentSettings.isFullScreen;
    }

    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
    }

    private void SetDropdownValue(int width, int height)
    {
        string targetResolution = width + " x " + height;
        int index = resolutionDropdown.options.FindIndex(option => option.text == targetResolution);
        if (index != -1)
        {
            resolutionDropdown.value = index;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        currentSettings.resolutionWidth = resolution.width;
        currentSettings.resolutionHeight = resolution.height;
        saveLoad.SaveSettings(currentSettings);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        currentSettings.volume = volume;
        saveLoad.SaveSettings(currentSettings);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        currentSettings.qualityLevel = qualityIndex;
        saveLoad.SaveSettings(currentSettings);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        currentSettings.isFullScreen = isFullscreen;
        saveLoad.SaveSettings(currentSettings);
    }
}
