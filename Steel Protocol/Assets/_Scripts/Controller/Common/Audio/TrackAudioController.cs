using UnityEngine;
using SteelProtocol.Manager;

namespace SteelProtocol.Controller.Common.Audio
{
    public class TrackAudioController : MonoBehaviour
    {
        // The volume of the track sound, between 0 and 1
        // 0 = silent, 1 = max volume
        [SerializeField] private float trackVolume = 0.5f;

        // The controller that will be used to get the current speed of the vehicle
        private MovementController movement;

        // The AudioSource that will play the track sound
        private AudioSource trackAudioSource;

        // The speed of the vehicle
        private float speed;
        
        // The last Y rotation of the vehicle
        private float lastYRotation;
        
        // Whether the vehicle was moving or not
        private bool wasMoving = false;


        private void Start()
        {
            // Get the movement controller
            movement = GetComponent<MovementController>();

            // Check if the movement controller is assigned
            if (movement == null)
                Debug.LogError("Missing MovementController for TrackAudioController");

            // Gets the last Y rotation of the vehicle
            lastYRotation = transform.eulerAngles.y;
        }


        private void FixedUpdate()
        {
            float pitch = CalculatePitch();

            float currentYRotation = transform.eulerAngles.y;
            float rotationSpeed = Mathf.Abs(Mathf.DeltaAngle(currentYRotation, lastYRotation));
            lastYRotation = currentYRotation;

            AdjustTrackPitch(pitch);
            CheckTrackMovement(speed, rotationSpeed);
        }


        // Calculates the pitch based on the current speed of the vehicle
        private float CalculatePitch()
        {
            speed = movement.GetCurrentSpeed();

            // E.g. If at 20 units of speed, sets the pitch to 1.2
            float pitch = 1f + (speed / 100f);

            return pitch;
        }


        // Adjusts the pitch of the track audio
        private void AdjustTrackPitch(float pitch)
        {
            if (trackAudioSource != null)
            {
                trackAudioSource.pitch = pitch;
            }
        }


        // Checks if the vehicle is moving and plays/stops the track sound accordingly
        private void CheckTrackMovement(float speed, float rotationSpeed)
        {
            bool isMoving = Mathf.Abs(speed) > 0.1f || rotationSpeed > 0.1f;

            if (isMoving && !wasMoving)
            {
                trackAudioSource = AudioManager.Instance.PlayLoopedSFX("Tracks", trackVolume);
            }
            else if (!isMoving && wasMoving)
            {
                AudioManager.Instance.StopLoopedSFX(trackAudioSource);
                trackAudioSource = null;
            }

            wasMoving = isMoving;
        }
    }
}
