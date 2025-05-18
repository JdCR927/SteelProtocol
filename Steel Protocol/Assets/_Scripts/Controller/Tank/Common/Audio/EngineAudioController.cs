using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Controller.Tank.Common.Audio
{
    [RequireComponent(typeof(MovementController))]
    public class EngineAudioController : MonoBehaviour
    {
        // The volume of the engine sound, between 0 and 1
        // 0 = silent, 1 = max volume
        [SerializeField] private float engineVolume = 0.2f;

        // The controller that will be used to get the current speed of the vehicle
        private MovementController movement;

        // The dedicated AudioSource that will play the engine sound
        private AudioSource engineAudioSource;


        private void Start()
        {
            // Get the movement controller
            movement = GetComponent<MovementController>();

            // Check if the movement controller is assigned
            if (movement == null)
                Debug.LogError("Missing MovementController for EngineAudioController");

            // Engine sound is played when the game starts and won't stop until the game ends
            engineAudioSource = AudioManager.Instance.PlayLoopedSFX("Engine", engineVolume);
        }


        private void FixedUpdate()
        {
            // Calculates the pitch
            float pitch = CalculatePitch();

            // Adjusts the pitch
            AdjustEnginePitch(pitch);
        }
        

        // Calculates the pitch based on the current speed of the vehicle
        private float CalculatePitch()
        {
            float speed = movement.GetCurrentSpeedAbsolute();

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
