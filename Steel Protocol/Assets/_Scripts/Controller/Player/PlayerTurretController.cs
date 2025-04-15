using UnityEngine;
using UnityEngine.InputSystem;

namespace SteelProtocol.Player
{
    public class PlayerTurretController: MonoBehaviour
    {

        #region Variables

        [Header("Turret Settings")]
        public Transform turret;
        public float rotationSpeed = 1f;

        private float yaw = 0f;
        private float mouseX = 0f;

        #endregion

        #region Builtin Methods

        void Start()
        {
            if (turret == null)
                Debug.LogError("Turret reference not set!");

            yaw = turret.localEulerAngles.y;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                mouseX = context.ReadValue<Vector2>().x;
            }
            else if (context.canceled)
            {
                mouseX = 0f;
            }
        }

        void FixedUpdate()
        {
            yaw += mouseX * rotationSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f, yaw, 0f);
            turret.localRotation = Quaternion.Slerp(turret.localRotation, targetRotation, Time.deltaTime * 10f);
        }

        #endregion
    }
}