using UnityEngine;
using SteelProtocol.Manager;

namespace SteelProtocol.Controller.Common.Audio
{
    [RequireComponent(typeof(MovementController))]
    public class EngineAudioController : MonoBehaviour
    {
        [SerializeField] private float engineVolume = 0.2f;

        private MovementController movement;
        private AudioSource engineAudioSource;

        private void Start()
        {
            movement = GetComponent<MovementController>();

            if (movement == null)
                Debug.LogError("Missing MovementController for EngineAudioController");

            engineAudioSource = AudioManager.Instance.PlayLoopedSFX("Engine", engineVolume);
        }

        private void FixedUpdate()
        {
            float speed = movement.GetCurrentSpeed();
            float pitch = 1f + (speed / 100f);

            AdjustEnginePitch(pitch);
        }

        private void AdjustEnginePitch(float pitch)
        {
            if (engineAudioSource != null)
            {
                engineAudioSource.pitch = pitch;
            }
        }
    }
}
