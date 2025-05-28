using UnityEngine;

namespace SteelProtocol.Controller.Tank.Common.Audio
{
    public class TankAudioPool : MonoBehaviour
    {
        [HideInInspector]
        public AudioSource[] sources;


        private void Awake()
        {
            // Auto-fetch AudioSources from children (or this GameObject)
            sources = GetComponentsInParent<AudioSource>();

            if (sources == null || sources.Length == 0)
            {
                Debug.LogError($"TankAudioPool: No AudioSources found on {gameObject.name}");
            }
        }
        

        public AudioSource GetSource(int index)
        {
            if (sources == null || index < 0 || index >= sources.Length)
                return null;
            return sources[index];
        }
    }
}