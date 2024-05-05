using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public UIButtonTransparency musicButtonTransparency;
    public UIButtonTransparency sfxButtonTransparency;
    private void Start()
    {
        LoadAudioPreferences();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        musicButtonTransparency.ToggleTransparency();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        sfxButtonTransparency.ToggleTransparency();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        SaveMusicVolume();
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
        SaveSFXVolume();
    }

    private void LoadAudioPreferences()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
