using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_MUTED_KEY = "MusicMuted";
    private const string SFX_MUTED_KEY = "SFXMuted";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadAudioPreferences();
        //PlayMusic("BgGame");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.songName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.songName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            musicSource.loop = false;
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        SaveMusicMutedState(musicSource.mute);
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        SaveSFXMutedState(sfxSource.mute);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveMusicVolume(volume);
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveSFXVolume(volume);
    }

    private void LoadAudioPreferences()
    {
        musicSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        sfxSource.volume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        musicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(MUSIC_MUTED_KEY, 0));
        sfxSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(SFX_MUTED_KEY, 0));
    }

    private void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    private void SaveSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    private void SaveMusicMutedState(bool muted)
    {
        PlayerPrefs.SetInt(MUSIC_MUTED_KEY, muted ? 1 : 0);
    }

    private void SaveSFXMutedState(bool muted)
    {
        PlayerPrefs.SetInt(SFX_MUTED_KEY, muted ? 1 : 0);
    }
}
