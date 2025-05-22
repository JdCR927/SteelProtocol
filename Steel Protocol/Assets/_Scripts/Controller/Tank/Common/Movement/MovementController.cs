using UnityEngine;
using SteelProtocol.Data.Engine;
using SteelProtocol.Data.Track;

namespace SteelProtocol.Controller.Tank.Common.Movement
{
    [RequireComponent(typeof(TrackMovementController))]
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class MovementController : MonoBehaviour
    {
        // Maximum forward/backward speed in units per second
        // Can in theory be surpassed if the tank were to be pushed by another object
        // Or if the tank were to be on a slope
        private float maxSpeed;
        private float acceleration;
        private float deceleration;
        private float rotationSpeed;

        private Rigidbody rb;
        private float currentSpeed = 0f;


        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody>();
        }


        public void InitializeEngine(EngineData engineData)
        {
            maxSpeed = engineData.maxSpeed;
            acceleration = engineData.acceleration;
            deceleration = engineData.deceleration;
        }


        public void InitializeTrack(TrackData trackData)
        {
            rotationSpeed = trackData.rotationSpeed;
        }


        public void Move(float forwardInput, float turnInput)
        {
            // Handle acceleration and deceleration based on input
            HandleAcceleration(forwardInput);

            // Calculate movement and rotation based on current speed and input
            Vector3 movement = currentSpeed * Time.fixedDeltaTime * transform.forward;
            Quaternion rotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);

            // Apply movement and rotation to the tank's Rigidbody
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }


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