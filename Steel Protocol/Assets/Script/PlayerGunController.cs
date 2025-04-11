using UnityEngine;

namespace SteelProtocol
{
    public class PlayerGunController : MonoBehaviour
    {
        public Transform gun;
        public float rotationSpeed = 50f;

        // Negative = raise, Positive = lower (Unity's inverted pitch)
        public float maxElevation = -15f;   // looking up
        public float minDepression = 5f;  // looking down

        private float pitch; // up/down angle

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
    }
}
