using UnityEngine;

namespace SteelProtocol
{
    [System.Serializable]
    public class SoundClip
    {
        // Name of the sound clip
        public string name;

        // Sound clip itself, whether music or SFX
        public AudioClip clip;
    }
}

