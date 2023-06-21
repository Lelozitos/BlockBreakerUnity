using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Slider slider;
    public TMP_Dropdown dropdown;

    void Start()
    {
        LoadValues();
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        LoadValues();
    }

    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("Quality", qualityIndex);
        LoadValues();
    }

    public void LoadValues()
    {
        //Mathf.Log10(volume) * 20
        slider.value = PlayerPrefs.GetFloat("Volume");
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        dropdown.value = PlayerPrefs.GetInt("Quality");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
    }
}
