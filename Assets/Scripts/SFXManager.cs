using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;
    public static SFXManager Instance => _instance;

    [SerializeField] AudioSource _SFXSource, _musicSource, _ambienceSource;

    private void Awake()
    {
        _instance = this;
    }

    public void PlaySFX(AudioClip clip, float pitch, float volume, float delay)
    {
        _SFXSource.Stop();
        _SFXSource.clip = clip;
        _SFXSource.pitch = pitch;
        _SFXSource.volume = volume;
        _SFXSource.PlayDelayed(delay);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _ambienceSource.volume = 0.05f;
    }

    public void PlayMusic()
    {
        _musicSource.Play();
        _ambienceSource.volume = 0.15f;
    }
}
