
using UnityEngine;

namespace SteelProtocol
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        #region Variables

        private Rigidbody rb;
        private float currentSpeed = 0f;
        public float moveSpeed = 15f;
        public float acceleration = 20f;
        public float decceleration = 50f;
        public float rotateSpeed = 120f;

        #endregion

        

        void HandleAcceleration()
        {
            float input = Input.GetAxis("Vertical");

            if (input != 0)
            {
                currentSpeed += input * acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, moveSpeed);
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= decceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0f);
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed += decceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Min(currentSpeed, 0f);
                }
            }
        }

        #region Builtin Methods

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            HandleAcceleration();

            float turn = Input.GetAxis("Horizontal") * rotateSpeed;

            Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turn * Time.fixedDeltaTime, 0f);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }

        #endregion
    }
}
