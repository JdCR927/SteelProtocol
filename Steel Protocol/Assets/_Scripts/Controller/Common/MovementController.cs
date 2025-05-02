using UnityEngine;

namespace SteelProtocol.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {
        [Header("Tank Movement Settings")]

        // Maximum forward/backward speed in units per second
        // Can in theory be surpassed if the tank were to be pushed by another object
        // Or if the tank were to be on a slope
        [SerializeField] private float maxSpeed = 10f;

        [SerializeField] private float acceleration = 10f;

        [SerializeField] private float deceleration = 10f;

        [SerializeField] private float rotateSpeed = 30f;


        private Rigidbody rb;

        private float currentSpeed = 0f;


        private void Awake()
        {
            // Get the Rigidbody component
            rb = GetComponent<Rigidbody>();
        }


        // Move the tank based on input
        public void Move(float forwardInput, float turnInput)
        {
            // Handle acceleration and deceleration based on input
            HandleAcceleration(forwardInput);

            // Calculate movement and rotation based on current speed and input
            Vector3 movement = currentSpeed * Time.fixedDeltaTime * transform.forward;
            Quaternion rotation = Quaternion.Euler(0f, turnInput * rotateSpeed * Time.fixedDeltaTime, 0f);

            // Apply movement and rotation to the tank's Rigidbody
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }


        // Accelerate or decelerate the tank based on input
        private void HandleAcceleration(float input)
        {
            if (input != 0)
            {
                // Accelerate towards the maximum speed in the direction of input
                // If input is positive, accelerate forward; if negative, accelerate backward
                currentSpeed += input * acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
            }
            else
            {
                // If no input, decelerate towards zero speed at the specified rate (deceleration)
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
            }
        }


        // Returns the current speed of the tank
        public float GetCurrentSpeed()
        {
            return currentSpeed;
        }


        // Returns the current ABSOLUTE (No negatives) speed of the tank
        public float GetCurrentSpeedAbsolute()
        {
            return Mathf.Abs(currentSpeed);
        }
    }
}