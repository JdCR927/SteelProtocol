using UnityEngine;
using SteelProtocol.Manager;

namespace SteelProtocol.Controller.Tank.Common.Audio
{
    public class TankAudioSource : MonoBehaviour
    {
        private AudioSource audioSource;

        private float localVolume = 1f;

        public void Initialize(AudioSource source, AudioClip clip, bool loop, float volume = 1f)
        {
            audioSource = source;
            audioSource.clip = clip;
            audioSource.loop = loop;
            localVolume = volume;
            audioSource.volume = localVolume * AudioManager.Instance.SfxVolume;

            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.minDistance = 20f;
            audioSource.maxDistance = 300f;
        }

        // Event methods
        private void OnEnable()
        {
            AudioManager.OnSettingsChanged += UpdateVolume;
        }

        private void OnDisable()
        {
            AudioManager.OnSettingsChanged -= UpdateVolume;
        }

        private void UpdateVolume()
        {
            if (audioSource != null)
                audioSource.volume = localVolume * AudioManager.Instance.SfxVolume;
        }
        // End of event methods

        public void Play()
        {
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();
        }

        public void Stop()
        {
            if (audioSource != null && audioSource.isPlaying)
                audioSource.Stop();
        }

        public void SetPitch(float pitch)
        {
            if (audioSource != null)
                audioSource.pitch = pitch;
        }

        public bool IsPlaying() => audioSource != null && audioSource.isPlaying;
        public bool IsNullOrStopped() => audioSource == null || !audioSource.isPlaying;
    }
}