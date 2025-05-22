using UnityEngine;
using SteelProtocol.Data.Turret;

namespace SteelProtocol.Controller.Tank.Common.Turret
{
    public class TurretController : MonoBehaviour
    {
        private Transform turret;
        private Transform gun;

        private float traverseSpeed;

        private float elevationSpeed;
        
        private readonly float maxElevation = -15f;
        private readonly float minDepression = 5f;

        private float yaw = 0f;
        private float pitch = 0f;


        private void Awake()
        {
            turret = GetComponent<TankController>().Turret;
            gun = GetComponent<TankController>().Muzzle;

            // Initialize the yaw and pitch angles based on the current local rotation of the turret and gun
            yaw = turret.localRotation.eulerAngles.y;
            pitch = gun.localRotation.eulerAngles.x;
        }


        public void Initialize(TurretData data)
        {
            traverseSpeed = data.traverseSpeed;
            elevationSpeed = data.elevationSpeed;
        }


        public void Aim(Vector2 look)
        {
            // Clamp the input vector to ensure it stays within the range of -1 to 1
            // This makes it so that the input is always the same, regardless of the input device or sensitivity
            look.x = Mathf.Clamp(look.x, -1f, 1f);
            look.y = Mathf.Clamp(look.y, -1f, 1f);

            // Changes the yaw and pitch angles based on the input vector
            // and the defined rotation and elevation speeds
            // Also clamps the pitch angle to the defined maximum and minimum values
            yaw += look.x * traverseSpeed * Time.deltaTime;
            pitch -= look.y * elevationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, maxElevation, minDepression);

            // Smoothly interpolates the turret and gun rotations to the new angles
            // using Slerp for a more natural rotation effect
            turret.localRotation = Quaternion.Slerp(turret.localRotation, Quaternion.Euler(0f, yaw, 0f), Time.deltaTime * 10f);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, Quaternion.Euler(pitch, 0f, 0f), Time.deltaTime * 10f);
        }
    }
}