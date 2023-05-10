using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    public static SettingsHandler Instance;
    public bool SoundOn;
    public bool MusicOn;
    public bool VibrateOn;
    public int Quality;

    public SettingsToggle soundToggle;
    public SettingsToggle musicToggle;
    public SettingsToggle qualityToggle;
    public SettingsToggle vibrateToggle;

    public UIView settingsView;

    private void Awake() {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void OnEnable() {
        loadSettings();
    }

    public void SaveSettings() {
        PlayerPrefs.SetInt("Sound", (SoundOn? 1: 0));
        PlayerPrefs.SetInt("Music", (MusicOn? 1: 0));
        PlayerPrefs.SetInt("Vibrate", (VibrateOn? 1: 0));
        PlayerPrefs.SetInt("Quality", Quality);
        Debug.Log("[SAVING SETTINGS]");
    }

    public void SaveDefault() {
        PlayerPrefs.SetInt("Sound", 1);
        PlayerPrefs.SetInt("Music", 1);
        PlayerPrefs.SetInt("Quality", 1);
         PlayerPrefs.SetInt("Vibrate", 1);
        Debug.Log("[SAVING SETTINGS]");
    }
    

    public void loadSettings() {
        SoundOn = PlayerPrefs.GetInt("Sound",1) == 0? false: true;
        MusicOn = PlayerPrefs.GetInt("Music",1) == 0? false: true;
        VibrateOn = PlayerPrefs.GetInt("Vibrate",1) == 0? false: true;
        Quality = PlayerPrefs.GetInt("Quality",2);
        Debug.Log("[LOADING SETTINGS]");
        UpdateUI();
    }

    public void UpdateUI() {
        soundToggle.SetToGivenState(SoundOn);
        musicToggle.SetToGivenState(MusicOn);
        vibrateToggle.SetToGivenState(VibrateOn);
        qualityToggle.ValueChangeCheck(Quality);
    }

     public void OpenSettings()
    {
        settingsView.Show();
    }
    public void CloseSettings()
    {
        settingsView.Hide();
    }
}
