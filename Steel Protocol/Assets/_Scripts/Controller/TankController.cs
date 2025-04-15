using UnityEngine;

namespace SteelProtocol
{
    public abstract class TankController : MonoBehaviour
    {

        #region Variables

        private Rigidbody rb;
        private float currentSpeed = 0f;

        [Header("Tank Movement Settings")]
        public float moveSpeed = 15f;
        public float acceleration = 20f;
        public float deceleration = 50f;
        public float rotateSpeed = 120f;

        #endregion

        #region Custom Methods

        protected void HandleAcceleration(float input)
        {
            if (input != 0)
            {
                currentSpeed += input * acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, moveSpeed);
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0f);
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed += deceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Min(currentSpeed, 0f);
                }
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract float GetForwardInput();
        protected abstract float GetTurnInput();

        #endregion

        #region Builtin Methods

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            float inputForward = GetForwardInput();
            float inputTurn = GetTurnInput();

            HandleAcceleration(inputForward);

            Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, inputTurn * rotateSpeed * Time.fixedDeltaTime, 0f);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }

        #endregion
    }

}
