using UnityEngine;
using SteelProtocol.Controller.Manager;

namespace SteelProtocol.Controller.Common.Audio
{
    [RequireComponent(typeof(MovementController))]
    public class TrackAudioController : MonoBehaviour
    {
        [SerializeField] private float trackVolume = 1f;

        private Rigidbody rb;
        private MovementController movement;
        private AudioSource trackAudioSource;

        private float lastYRotation;
        private bool wasMoving = false;

        private void Start()
        {
            movement = GetComponent<MovementController>();

            if (movement == null)
                Debug.LogError("Missing MovementController for TrackAudioController");

            lastYRotation = transform.eulerAngles.y;
        }

        private void FixedUpdate()
        {
            float speed = movement.GetCurrentSpeed();
            float pitch = 1f + (speed / 100f);

            float currentYRotation = transform.eulerAngles.y;
            float rotationSpeed = Mathf.Abs(Mathf.DeltaAngle(currentYRotation, lastYRotation));
            lastYRotation = currentYRotation;

            AdjustTrackPitch(pitch);
            CheckTrackMovement(speed, rotationSpeed);
        }

        private void AdjustTrackPitch(float pitch)
        {
            if (trackAudioSource != null)
            {
                trackAudioSource.pitch = pitch;
            }
        }

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
