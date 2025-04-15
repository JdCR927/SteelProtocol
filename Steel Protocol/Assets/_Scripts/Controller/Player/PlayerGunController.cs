using UnityEngine;
using UnityEngine.InputSystem;

namespace SteelProtocol.Player
{
    public class PlayerGunController : MonoBehaviour
    {

        #region Variables

        [Header("Gun Settings")]
        public Transform gun;
        public float rotationSpeed = 1f;
        public float maxElevation = -15f;
        public float minDepression = 5f;

        private float pitch = 0f;
        private float mouseY = 0f;

        #endregion
        
        #region Builtin Methods

        void Start()
        {
            if (gun == null)
                Debug.LogError("Gun reference not set!");

            pitch = gun.localEulerAngles.x;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                mouseY = context.ReadValue<Vector2>().y;
            }
            else if (context.canceled)
            {
                mouseY = 0f;
            }
        }

        void FixedUpdate()
        {
            pitch -= mouseY * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, maxElevation, minDepression);
            Quaternion targetRotation = Quaternion.Euler(pitch, 0f, 0f);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, targetRotation, Time.deltaTime * 10f);
        }

        #endregion
    }
}
