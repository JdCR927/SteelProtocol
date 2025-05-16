using UnityEngine;

namespace SteelProtocol.Controller
{
    public class TurretController: MonoBehaviour
    {
        // Transform of the turret to rotate horizontally
        // Assigned in the inspector
        private Transform turret;

        // Units per second for horizontal rotation speed
        [SerializeField] private float rotationSpeed = 20f;


        // Transform of the gun to adjust elevation
        // Assigned in the inspector
        private Transform gun;

        // Units per second for elevation speed
        [SerializeField] private float elevationSpeed = 20f;

        // Maximum upward angle in degrees.
        [SerializeField] private float maxElevation = -15f;

        // Minimum downward angle in degrees.
        [SerializeField] private float minDepression = 5f;


        // Yaw angle for the turret's rotation
        private float yaw = 0f;

        // Pitch angle for the gun's elevation
        private float pitch = 0f;


        private void Awake()
        {
            turret = GetComponent<TankController>().Turret;
            gun = GetComponent<TankController>().Muzzle;

            // Initialize the yaw and pitch angles based on the current local rotation of the turret and gun
            yaw = 0f;
            pitch = 0f;
        }


        // Updates the turret and gun rotation based on the input vector
        public void Aim(Vector2 look)
        {
            // Clamp the input vector to ensure it stays within the range of -1 to 1
            // This makes it so that the input is always the same, regardless of the input device or sensitivity
            look.x = Mathf.Clamp(look.x, -1f, 1f);
            look.y = Mathf.Clamp(look.y, -1f, 1f);

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