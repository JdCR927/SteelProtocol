using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Common.Audio
{
    public class TurretAudioController : MonoBehaviour
    {
        // The volume of the turret sound, between 0 and 1
        // 0 = silent, 1 = max volume
        [SerializeField] private float turretVolume = 0.5f;

        // Input interface to check for turret movement
        private IInputSource input;

        // The AudioSource that will play the turret sound
        private AudioSource turretAudioSource;


        private void FixedUpdate()
        {
            // Gets the input
            input = GetComponent<IInputSource>();

            // Checks for turret input
            CheckTurretMovement(input.GetLookInput());

            // Adjusts the pitch of the audio
            AdjustTurretPitch();
        }
        

        // Checks if the turret is moving and plays/stops the sound accordingly
        private void CheckTurretMovement(Vector2 input)
        {
            // Checks if the turret is moving
            bool isMoving = Mathf.Abs(input.x) > 0.1f || Mathf.Abs(input.y) > 0.1f;

            if (isMoving)
            {
                // If audio is already playing, ignore
                if (turretAudioSource == null)
                {
                    // Play the turret sound if it is not already playing
                    turretAudioSource = AudioManager.Instance.PlayLoopedSFX("Turret", turretVolume);
                }
            }
            else
            {
                if (turretAudioSource != null)
                {
                    // Stop the turret sound if it is playing
                    AudioManager.Instance.StopLoopedSFX(turretAudioSource);
                    turretAudioSource = null;
                }
            }
        }


        // Adjusts the pitch of the turret audio
        private void AdjustTurretPitch()
        {
            float pitch = Random.Range(0.90f, 1.05f);

            // Check if turretAudioSource is null or not and adjusts the pitch accordingly
            if (turretAudioSource != null)
            {
                // Changes pitch
                turretAudioSource.pitch = pitch;
            }
        }
    }
}
