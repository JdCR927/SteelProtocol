using UnityEngine;
using System;
using SteelProtocol.Data.Save;

namespace SteelProtocol.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        // Arrays to hold music and sound effect clips
        // These should be assigned in the Unity Inspector
        public SoundClip[] music, sfx;

        public float MusicVolume { get; private set; }
        public float SfxVolume { get; private set; }

        public static event Action OnSettingsChanged;

        private void Start()
        {
            var audioSettings = AudioSaveSystem.LoadAudioSettings();
            ApplyAudioSettings(audioSettings);
        }

        private void ApplyAudioSettings(AudioSaveData audioSettings)
        {
            MusicVolume = audioSettings.musicVolume;
            SfxVolume = audioSettings.sfxVolume;

            OnSettingsChanged?.Invoke();
        }

        public AudioClip GetMusicClip(string name)
        {
            return Array.Find(music, m => m.name == name)?.clip;
        }

        public AudioClip GetSFXClip(string name)
        {
            return Array.Find(sfx, s => s.name == name)?.clip;
        }
    }
}
