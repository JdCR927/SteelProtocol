using UnityEngine;

namespace SteelProtocol
{
    public class PlayerGunController : MonoBehaviour
    {

        #region Variables

        public Transform gun; // Muzzle/Gun
        public float rotationSpeed = 50f; // Vertical Rotation Speed
        public float maxElevation = -15f;   // Positive Vertical Guidance
        public float minDepression = 5f;  // Negative Vertical Guidance
        private float pitch;

        #endregion
        
        #region Builtin Methods

        void Start()
        {
            if (gun == null)
            {
                Debug.LogError("Gun reference not set!");
            }

            pitch = gun.localEulerAngles.x;
        }

        void Update()
        {
            float mouseY = Input.GetAxis("Mouse Y");

            // Invert to behave like normal FPS look controls
            pitch -= mouseY * rotationSpeed * Time.deltaTime;

            // Clamp between elevation limits
            pitch = Mathf.Clamp(pitch, maxElevation, minDepression);

            // Apply rotation
            Quaternion targetRotation = Quaternion.Euler(pitch, 0f, 0f);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, targetRotation, Time.deltaTime * 10f);
        }

        #endregion
    }
}
