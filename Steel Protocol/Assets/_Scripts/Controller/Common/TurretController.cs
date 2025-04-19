using SteelProtocol.Controller.Manager;
using UnityEngine;

namespace SteelProtocol.Controller
{
    public class TurretController: MonoBehaviour
    {
        [Header("Turret Settings")]
        [Tooltip("Transform of the turret to rotate horizontally.")]
        public Transform turret;
        [Tooltip("Speed of turret horizontal rotation.")]
        public float rotationSpeed = 1f;


        [Header("Gun Settings")]
        [Tooltip("Transform of the gun to adjust elevation.")]
        public Transform gun;
        [Tooltip("Speed of gun vertical elevation.")]
        public float elevationSpeed = 1f;
        [Tooltip("Maximum upward angle in degrees.")]
        public float maxElevation = -15f;
        [Tooltip("Maximum downward angle in degrees.")]
        public float minDepression = 5f;


        private float yaw = 0f;
        private float pitch = 0f;



        private void Awake()
        {
            if (turret == null || gun == null)
                Debug.LogError("Turret or gun not assigned!");

            yaw = turret.localEulerAngles.y;
            pitch = gun.localEulerAngles.x;
        }

        public void Aim(Vector2 look)
        {
            yaw += look.x * rotationSpeed * Time.deltaTime;
            pitch -= look.y * elevationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, maxElevation, minDepression);

            turret.localRotation = Quaternion.Slerp(turret.localRotation, Quaternion.Euler(0f, yaw, 0f), Time.deltaTime * 10f);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, Quaternion.Euler(pitch, 0f, 0f), Time.deltaTime * 10f);
        }

        public float GetYaw()
        {
            return yaw;
        }
        public float GetPitch()
        {
            return pitch;
        }
    }
}