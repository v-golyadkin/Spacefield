using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private SoundLibrary _sfxLibrary;
    [SerializeField] private AudioSource _sfx2DSource;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound3D(AudioClip clip, Vector3 position)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }

    public void PlaySound3D(string soundName, Vector3 position)
    {
        PlaySound3D(_sfxLibrary.GetClipFromName(soundName), position);
    }

    public void PlaySound2D(string soundName)
    {
        _sfx2DSource.PlayOneShot(_sfxLibrary.GetClipFromName(soundName), _sfxLibrary.GetVolume(soundName));
    }
}


