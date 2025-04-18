using UnityEngine;

namespace SteelProtocol.Controller
{
    /// <summary>
    /// Handles Rigidbody-based movement logic for tanks, including acceleration and rotation.
    /// Can be reused by both player and AI tanks.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {
        [Header("Tank Movement Settings")]
        [Tooltip("Maximum forward/backward speed.")]
        public float moveSpeed = 15f;

        [Tooltip("Rate of acceleration when input is applied.")]
        public float acceleration = 20f;

        [Tooltip("Rate of deceleration when no input is applied.")]
        public float deceleration = 50f;

        [Tooltip("Turning speed of the tank.")]
        public float rotateSpeed = 30f;

        private Rigidbody rb;
        private float currentSpeed = 0f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Applies movement and rotation based on input.
        /// </summary>
        /// <param name="forwardInput">Forward/backward movement input.</param>
        /// <param name="turnInput">Left/right rotation input.</param>
        public void Move(float forwardInput, float turnInput)
        {
            HandleAcceleration(forwardInput);

            Vector3 movement = currentSpeed * Time.fixedDeltaTime * transform.forward;
            Quaternion rotation = Quaternion.Euler(0f, turnInput * rotateSpeed * Time.fixedDeltaTime, 0f);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }

        /// <summary>
        /// Applies acceleration or deceleration to the tank based on input.
        /// </summary>
        /// <param name="input">Current forward/backward input.</param>
        private void HandleAcceleration(float input)
        {
            if (input != 0)
            {
                currentSpeed += input * acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, moveSpeed);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
            }
        }
    }
}