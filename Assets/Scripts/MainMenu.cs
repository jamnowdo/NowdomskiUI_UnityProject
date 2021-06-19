using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loadMenu;
    public GameObject audioMenu;
    public GameObject visualMenu;
    public GameObject controlsMenu;
    public GameObject alertBox;
    public AudioMixer mixer;
    public Slider masterVolumeSlider;

    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void DisplayOptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DisplayAudioMenu()
    {
        audioMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void closeAudioMenu()
    {
        optionsMenu.SetActive(true);
        audioMenu.SetActive(false);
        alertBox.SetActive(false);
    }

    public void DisplayVisualMenu()
    {
        visualMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void closeVisualMenu()
    {
        optionsMenu.SetActive(true);
        visualMenu.SetActive(false);
    }

    public void DispalyControlsMenu()
    {
        controlsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void closeControlsMenu()
    {
        optionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void closeOptionsMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        alertBox.SetActive(false);
    }

    public void DisplayLoadMenu()
    {
        loadMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void closeLoadMenu()
    {
        mainMenu.SetActive(true);
        loadMenu.SetActive(false);
    }

    public void StartNewGame()
    {
        // SceneManager.LoadScene("Level");
        SceneManager.LoadScene(1);
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void SaveOptions()
    {
        Debug.Log("Saving Options");
        float mixerMasterVolume;
        mixer.GetFloat("MasterVolume", out mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", mixerMasterVolume);
        PlayerPrefs.SetFloat("MasterVolumeSlider", masterVolumeSlider.value);
    }

    public void LoadOptions()
    {
        Debug.Log("Loading Options");
        float mixerMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        mixer.SetFloat("MasterVolume", mixerMasterVolume);
        float masterSliderValue = PlayerPrefs.GetFloat("MasterVolumeSlider", 1f);
        masterVolumeSlider.value = masterSliderValue;
    }

    private void Start()
    {
        LoadOptions();
    }

    public void CheckForChanges()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float actualMasterVolume;
        mixer.GetFloat("MasterVolume", out actualMasterVolume);
        if (Mathf.Approximately(savedMasterVolume, actualMasterVolume))
        {
            //The values are the same.
            closeAudioMenu();
        }
        else
        {
            //The values are different.
            ShowAlertBox();
        }
    }

    public void ShowAlertBox()
    {
        audioMenu.SetActive(false);
        alertBox.SetActive(true);
    }
}
