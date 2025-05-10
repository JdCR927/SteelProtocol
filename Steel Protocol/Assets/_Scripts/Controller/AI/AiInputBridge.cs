using UnityEngine;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.AI
{
    public class AiInputBridge : MonoBehaviour, IInputSource
    {
        private Transform turret;
        private Transform muzzle;

        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;
        private bool fireSecondary;
        private bool fireTertiary;


        public void Awake()
        {
            turret = GetComponent<TankController>().Turret;
            muzzle = GetComponent<TankController>().Muzzle;

            if (turret == null)
                Debug.LogError("Missing turret transform.");
            if (muzzle == null)
                Debug.LogError("Missing muzzle transform.");
        }

        /* TODO:
        public void OnMove( context)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        */


        public void OnLook(GameObject target)
        {
            if (target == null) return;

            Vector3 toTarget = (target.transform.position - turret.position).normalized;

            // Horizontal (yaw)
            Vector3 flatTurretFwd = Vector3.ProjectOnPlane(turret.forward, Vector3.up).normalized;
            Vector3 flatToTarget = Vector3.ProjectOnPlane(toTarget, Vector3.up).normalized;
            float yawDelta = Vector3.SignedAngle(flatTurretFwd, flatToTarget, Vector3.up);

            // Vertical (pitch)
            Vector3 gunForward = muzzle.forward;
            float pitchDelta = Vector3.SignedAngle(gunForward, toTarget, transform.right);

            lookInput = new Vector2(yawDelta, pitchDelta);
        }


        /* TODO:
        public void OnAttack1( context)
        {
            fireMain = context.ReadValue<float>() > 0.5f;
        }
        */

        /* TODO:
        public void OnAttack2( context)
        {
            fireSecondary = context.ReadValue<float>() > 0.5f;
        }
        */


        /* TODO:
        public void OnAttack3( context)
        {
            fireTertiary = context.ReadValue<float>() > 0.5f;
        }
        */


        public float GetForwardInput() => movementInput.y;


        public float GetTurnInput() => movementInput.x;


        public Vector2 GetLookInput() => lookInput;


        public bool IsFiringMain() => fireMain;


        public bool IsFiringSec() => fireSecondary;
        
        
        public bool IsFiringTer() => fireTertiary;
    }
}
