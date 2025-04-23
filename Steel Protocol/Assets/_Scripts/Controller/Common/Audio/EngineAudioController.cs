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
            
            // Engine sound is played when the game starts and won't stop until the game ends
            engineAudioSource = AudioManager.Instance.PlayLoopedSFX("Engine", engineVolume);
        }

        private void FixedUpdate()
        {
            float pitch = CalculatePitch();

            AdjustEnginePitch(pitch);
        }

        // Calculates the pitch based on the current speed of the vehicle
        private float CalculatePitch()
        {
            float speed = movement.GetCurrentSpeed();

            // E.g. If at 20 units of speed, sets the pitch to 1.2
            float pitch = 1f + (speed / 100f);

            return pitch;
        }

        // Adjusts the pitch of the engine audio
        private void AdjustEnginePitch(float pitch)
        {
            // Check if engineAudioSource is null or not and adjusts the pitch accordingly
            if (engineAudioSource != null)
            {
                engineAudioSource.pitch = pitch;
            }
        }
    }
}
