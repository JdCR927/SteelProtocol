///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// TODO: WARNING: THIS IS NOT A GOOD WAY TO DO THIS! WHEN MULTIPLE TANKS ARE PLAYING THE SAME SOUND, THEY CAN BE HEARD AT THE SAME TIME REGARDLESS OF THE DISTANCE TO THE PLAYER WHICH CAN BE AWFUL! //
// TODO:                                                                    EITHER REWORK IT, OR USE SOMETHING ELSE                                                                                  //
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;

namespace SteelProtocol.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        // Arrays to hold music and sound effect clips
        // These should be assigned in the Unity Inspector
        public SoundClip[] music, sfx;
        
        // Two AudioSource references for music and sound effects
        // These should be assigned in the Unity Inspector
        public AudioSource musicSource, sfxSource;


        // Searches for the name of the music and plays it
        public void PlayMusic(string name)
        {
            // Find the music by name in the music array
            SoundClip sound = Array.Find(music, s => s.name == name);

            if (sound != null)
            {
                // Play the music clip
                musicSource.clip = sound.clip;
                musicSource.Play();
            }else 
            {
                // If the sound is not found, log a warning
                Debug.LogWarning($"Sound {name} not found in music array.");
            }
        }


        // Searches for the name of the sfx and plays it
        public void PlaySFX(string name, float volume)
        {
            // Find the sfx by name in the sfx array
            SoundClip sound = Array.Find(sfx, s => s.name == name);

            if (sound != null)
            {
                // Create a new AudioSource instance for the sfx
                AudioSource sfxInstance = Instantiate(sfxSource);

                // Set the AudioSource clip to the selected sound
                sfxInstance.clip = sound.clip;

                // Set the AudioSource volume to the specified value
                sfxInstance.volume = volume;

                // Play the sound
                sfxInstance.Play();

                // Destroy the AudioSource instance after the sound has finished playing
                Destroy(sfxInstance.gameObject, sound.clip.length);
            }else 
            {
                // If the sound is not found, log a warning
                Debug.LogWarning($"Sound {name} not found in SFX array.");
            }
        }


        // Searches for the name of the sfx and plays it in a loop
        // Returns the AudioSource instance so it can be stopped later
        public AudioSource PlayLoopedSFX(string name, float volume)
        {
            // Find the sfx by name in the sfx array
            SoundClip sound = Array.Find(sfx, s => s.name == name);

            if (sound != null)
            {
                // Create a new AudioSource instance for the looped sfx
                AudioSource sfxInstance = Instantiate(sfxSource);

                // Set the AudioSource clip to the selected sound
                sfxInstance.clip = sound.clip;

                // Set the AudioSource volume to the specified value
                sfxInstance.volume = volume;

                // Set the AudioSource to loop the sound
                sfxInstance.loop = true;

                // Play the looped sound
                sfxInstance.Play();

                // Return the AudioSource instance for later use (e.g., stopping it)
                return sfxInstance;
            }else 
            {
                // If the sound is not found, log a warning and return null
                Debug.LogWarning($"Sound {name} not found in SFX array.");
                return null;
            }
        }


        public void StopLoopedSFX(AudioSource sfxInstance)
        {
            // Check if the sfxInstance is not null before stopping and destroying it
            if (sfxInstance != null)
            {
                // Stop the looped sfx and destroy the instance
                sfxInstance.Stop();
                Destroy(sfxInstance.gameObject);
            }
        }
    }
}
