using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public string name;
    public AudioClip clip;
    public float volume;
}

public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;

    public AudioClip GetClipFromName(string name)
    {
        foreach(var soundEffect in soundEffects)
        {
            if(soundEffect.name == name)
            {
                return soundEffect.clip;
            }
        }

        return null;
    }

    public float GetVolume(string name)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.name == name)
            {
                return soundEffect.volume;
            }
        }

        return 0;
    }
}
