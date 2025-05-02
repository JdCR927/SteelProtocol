using SteelProtocol.Manager;
using UnityEngine;

namespace SteelProtocol.Controller
{
    public class TurretController: MonoBehaviour
    {
        [Header("Turret Settings")]

        // Transform of the turret to rotate horizontally
        // Assigned in the inspector
        [Tooltip("Transform of the turret to rotate horizontally.")]
        [SerializeField] private Transform turret;

        // Units per second for horizontal rotation speed
        [Tooltip("Speed of turret horizontal rotation.")]
        [SerializeField] private float rotationSpeed = 1f;


        [Header("Gun Settings")]

        // Transform of the gun to adjust elevation
        // Assigned in the inspector
        [Tooltip("Transform of the gun to adjust elevation.")]
        [SerializeField] private Transform gun;

        // Units per second for elevation speed
        [Tooltip("Speed of gun vertical elevation.")]
        [SerializeField] private float elevationSpeed = 1f;

        // Maximum upward angle in degrees.
        [Tooltip("Maximum upward angle in degrees. E.g. -15 means the gun can only go down 15 degrees upwards.")]
        [SerializeField] private float maxElevation = -15f;

        // Minimum downward angle in degrees.
        [Tooltip("Maximum downward angle in degrees. E.g. 5 means the gun can only go down 5 degrees downwards.")]
        [SerializeField] private float minDepression = 5f;

        // Yaw angle for the turret's rotation
        private float yaw = 0f;

        // Pitch angle for the gun's elevation
        private float pitch = 0f;


        private void Awake()
        {
            // Check if the turret and gun transforms are assigned
            if (turret == null || gun == null)
                Debug.LogError("Turret or gun not assigned!");

            // Initialize the yaw and pitch angles based on the current local rotation of the turret and gun
            yaw = turret.localEulerAngles.y;
            pitch = gun.localEulerAngles.x;
        }


        // Updates the turret and gun rotation based on the input vector
        public void Aim(Vector2 look)
        {
            // Changes the yaw and pitch angles based on the input vector
            // and the defined rotation and elevation speeds
            // Also clamps the pitch angle to the defined maximum and minimum values
            yaw += look.x * rotationSpeed * Time.deltaTime;
            pitch -= look.y * elevationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, maxElevation, minDepression);

            // Smoothly interpolates the turret and gun rotations to the new angles
            // using Slerp for a more natural rotation effect
            turret.localRotation = Quaternion.Slerp(turret.localRotation, Quaternion.Euler(0f, yaw, 0f), Time.deltaTime * 10f);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, Quaternion.Euler(pitch, 0f, 0f), Time.deltaTime * 10f);
        }
    }
}