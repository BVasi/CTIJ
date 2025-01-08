using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, MAX_VOLUME_VALUE);
            Load();
        }
        else
        {
            Load();
        }

    }
    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }

    private void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, VolumeSlider.value);
    }

    [SerializeField] private Slider VolumeSlider;
    private const string MUSIC_VOLUME = "musicVolume";
    private const float MAX_VOLUME_VALUE = 1f;
}
