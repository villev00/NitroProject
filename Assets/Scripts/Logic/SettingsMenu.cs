using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider, soundSlider;

    public AudioClip buttonSoundClip;
    public AudioClip menuMusicClip;

    const string MUSIC_VOL_KEY = "MusicVolume";
    const string SOUND_VOL_KEY = "SoundVolume";

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 0.15f);
        soundSlider.value = PlayerPrefs.GetFloat(SOUND_VOL_KEY, 0.08f);
        AudioManager.Mixer.SetFloat(MUSIC_VOL_KEY, Mathf.Log10(musicSlider.value) * 20);
        AudioManager.Mixer.SetFloat(SOUND_VOL_KEY, Mathf.Log10(soundSlider.value) * 20);

        AudioManager.PlayMusic(menuMusicClip);
    }

    public void SetLevelMusic()
    {
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, musicSlider.value);
        AudioManager.Mixer.SetFloat(MUSIC_VOL_KEY, Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetLevelSound()
    {
        PlayerPrefs.SetFloat(SOUND_VOL_KEY, soundSlider.value);
        AudioManager.Mixer.SetFloat(SOUND_VOL_KEY, Mathf.Log10(soundSlider.value) * 20);
    }

    public void PlayButtonSound()
    {
        AudioManager.PlaySound(buttonSoundClip, false);
    }
}
