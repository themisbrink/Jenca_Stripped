using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 using DarkTonic.MasterAudio;
// using UnityEngine.Rendering.Universal;

public class SettingsToggle : MonoBehaviour
{
    public enum ToggleSetting
    {
        Alarm,
        Sound,
        Music,
        Vibration,
        Quality 
    }
    public ToggleSetting settingType;

    public Image imageOn;
    public Image imageOff;

    public bool ToggleState;
    public Animator m_animator;

    private void OnEnable()
    {
        SetToSTate();
    }
    public void ToggleButton()
    {
        if(m_animator == null) return;
        if (ToggleState)
        {
            ToggleState = false;
            m_animator.Play("ToggleOff");
            imageOn.gameObject.SetActive(false);
            imageOff.gameObject.SetActive(true);
        }
        else
        {
            ToggleState = true;
            m_animator.Play("ToggleOn");
            imageOn.gameObject.SetActive(true);
            imageOff.gameObject.SetActive(false);
        }

        switch (settingType)
        {
            case ToggleSetting.Alarm:
                Debug.Log("Alams set to " + ToggleState);
                break;
            case ToggleSetting.Music:
                Debug.Log("Music set to " + ToggleState);
               MasterAudio.PlaylistMasterVolume = ToggleState == true ? 1 : 0;
                ////MasterAudio.SetBusVolumeByName("Music", ToggleState == true ? 1 : 0);
                SettingsHandler.Instance.MusicOn = ToggleState;
                break;
            case ToggleSetting.Sound:
                Debug.Log("Sound set to " + ToggleState);
               MasterAudio.SetBusVolumeByName("SFX", ToggleState == true ? 1 : 0);
                SettingsHandler.Instance.SoundOn = ToggleState;
                break;
            case ToggleSetting.Vibration:
                Debug.Log("Vibration set to " + ToggleState);
                SettingsHandler.Instance.VibrateOn = ToggleState;
                break;
        }

        SettingsHandler.Instance.SaveSettings();

    }

    public void SetToSTate()
    {
        if(m_animator == null) return;
        if (!ToggleState)
        {

            m_animator.Play("ToggleOff");
            imageOn.gameObject.SetActive(false);
            imageOff.gameObject.SetActive(true);
        }
        else
        {
            m_animator.Play("ToggleOn");
            imageOn.gameObject.SetActive(true);
            imageOff.gameObject.SetActive(false);
        }

        switch (settingType)
        {
            case ToggleSetting.Alarm:
                // Debug.Log("Alams set to " + ToggleState);
                break;
            case ToggleSetting.Music:
                // Debug.Log("Music set to " + ToggleState);
                break;
            case ToggleSetting.Sound:
                // Debug.Log("Sound set to " + ToggleState);
               //MasterAudio.SetBusVolumeByName("SFX", ToggleState == true ? 1 : 0);
                break;
            case ToggleSetting.Vibration:
                // Debug.Log("Vibration set to " + ToggleState);
                break;
        }
    }

    public void SetToGivenState(bool state){
        ToggleState = state;
        if(m_animator == null) return;
        if (!ToggleState)
        {

            m_animator.Play("ToggleOff");
            imageOn.gameObject.SetActive(false);
            imageOff.gameObject.SetActive(true);
        }
        else
        {
            m_animator.Play("ToggleOn");
            imageOn.gameObject.SetActive(true);
            imageOff.gameObject.SetActive(false);
        }

        switch (settingType)
        {
            case ToggleSetting.Alarm:
                // Debug.Log("Alams set to " + ToggleState);
                break;
            case ToggleSetting.Music:
               MasterAudio.PlaylistMasterVolume = ToggleState == true ? 1 : 0;
                ////MasterAudio.SetBusVolumeByName("Music", ToggleState == true ? 1 : 0);
                break;
            case ToggleSetting.Sound:
                // Debug.Log("Sound set to " + ToggleState);
               MasterAudio.SetBusVolumeByName("SFX", ToggleState == true ? 1 : 0);
                break;
            case ToggleSetting.Vibration:
                // Debug.Log("Vibration set to " + ToggleState);
                break;
        }
    }

    public Slider mainSlider;
    public TMPro.TextMeshProUGUI qualityLabel;
    public Light mainLight;
    // Invoked when the value of the slider changes.
    public void ValueChangeCheck(int value)
    {
        switch (value){
            case 0:
                qualityLabel.text = "Low Quality";
                // QualitySettings.SetQualityLevel(0, true);
                // Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                mainLight.shadows = LightShadows.None;
                SettingsHandler.Instance.Quality = 0;
                mainSlider.value = 0;
            break;
            case 1:
                qualityLabel.text = "Medium Quality";
                // QualitySettings.SetQualityLevel(2, true);
                // Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
                // Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.Low;
                 mainLight.shadows = LightShadows.Hard;
                SettingsHandler.Instance.Quality = 1;
                 mainSlider.value = 1;
            break;
            case 2:
                qualityLabel.text = "Great Quality";
                // QualitySettings.SetQualityLevel(5, true);
                // Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                // Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.High;
                 mainLight.shadows = LightShadows.Hard;
                SettingsHandler.Instance.Quality = 2;
                 mainSlider.value = 2;
            break;
        }

        SettingsHandler.Instance.SaveSettings();
    }
    
}
