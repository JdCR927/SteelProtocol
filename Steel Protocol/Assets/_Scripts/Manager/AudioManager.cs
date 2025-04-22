using UnityEngine;
using System;

namespace SteelProtocol.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        public Sound[] music, sfx;
        public AudioSource musicSource, sfxSource;

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(music, s => s.name == name);
            if (sound != null)
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }else 
            {
                Debug.LogWarning($"Sound {name} not found in music array.");
            }
        }

        public void PlaySFX(string name, float volume)
        {
            Sound sound = Array.Find(sfx, s => s.name == name);

            if (sound != null)
            {
                AudioSource sfxInstance = Instantiate(sfxSource);

                sfxInstance.clip = sound.clip;
                sfxInstance.volume = volume;
                sfxInstance.Play();
                Destroy(sfxInstance.gameObject, sound.clip.length);
            }else 
            {
                Debug.LogWarning($"Sound {name} not found in SFX array.");
            }
        }

        public AudioSource PlayLoopedSFX(string name, float volume)
        {
            Sound sound = Array.Find(sfx, s => s.name == name);

            if (sound != null)
            {
                AudioSource sfxInstance = Instantiate(sfxSource);

                sfxInstance.clip = sound.clip;
                sfxInstance.volume = volume;
                sfxInstance.loop = true;
                sfxInstance.Play();
                return sfxInstance;
            }else 
            {
                Debug.LogWarning($"Sound {name} not found in SFX array.");
                return null;
            }
        }

        public void StopLoopedSFX(AudioSource sfxInstance)
        {
            if (sfxInstance != null)
            {
                sfxInstance.Stop();
                Destroy(sfxInstance.gameObject);
            }
        }
    }
}