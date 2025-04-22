using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Common.Audio
{
    [RequireComponent(typeof(TurretController))]
    public class TurretAudioController : MonoBehaviour
    {
        [SerializeField] private float turretVolume = 1f;

        private IInputSource input;
        private TurretController turret;
        private AudioSource turretAudioSource;
        private float pitch;

        private void Start()
        {
            turret = GetComponent<TurretController>();

            if (turret == null)
                Debug.LogError("Missing TurretController for TurretAudioController");
        }

        private void FixedUpdate()
        {
            // Gets the input
            input = GetComponent<IInputSource>();

            // Checks for turret input
            CheckTurretMovement(input.GetLookInput());

            // Random float between 0.90 and 1.05
            pitch = Random.Range(0.90f, 1.05f);

            // Adjusts the pitch of the audio
            AdjustTurretPitch(pitch);
        }

        
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


        private void AdjustTurretPitch(float pitch)
        {
            // Check if turretAudioSource is null or not
            if (turretAudioSource != null)
            {
                // Changes pitch
                turretAudioSource.pitch = pitch;
            }
        }
    }
}
