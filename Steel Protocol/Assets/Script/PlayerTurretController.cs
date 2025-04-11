using UnityEngine;

namespace SteelProtocol
{
    public class PlayerTurretController: MonoBehaviour
    {
        public Transform turret;
            public float rotationSpeed = 50f;

            private float yaw;

            void Start()
            {
                if (turret == null)
                {
                    Debug.LogError("Turret reference not set!");
                }

                yaw = turret.localEulerAngles.y;
            }

            void Update()
            {
                float mouseX = Input.GetAxis("Mouse X");

                // Update desired yaw
                yaw += mouseX * rotationSpeed * Time.deltaTime;

                // Apply rotation
                Quaternion targetRotation = Quaternion.Euler(0f, yaw, 0f);
                turret.localRotation = Quaternion.Slerp(turret.localRotation, targetRotation, Time.deltaTime * 10f);
                
            }
    }
}