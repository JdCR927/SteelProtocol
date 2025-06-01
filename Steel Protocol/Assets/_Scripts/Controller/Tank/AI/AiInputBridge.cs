using UnityEngine;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Tank.AI
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

        public void OnMove(Vector3 waypoint)
        {
            Vector3 toWaypoint = waypoint - transform.parent.position;

            float forwardInput = CalculateForwardInput(toWaypoint);

            float turnInput = CalculateTurnInput(toWaypoint);

            movementInput = new Vector2(forwardInput, turnInput);
        }

        private float CalculateForwardInput(Vector3 toWaypoint)
        {
            Vector3 forward = Vector3.ProjectOnPlane(transform.parent.forward, Vector3.up).normalized;
            Vector3 direction = Vector3.ProjectOnPlane(toWaypoint, Vector3.up).normalized;

            float angle = Vector3.Angle(forward, direction); // 0° is forward, 180° is behind

            float distance = toWaypoint.magnitude;

            // If we're close, fine-tune forward/back
            if (distance <= 50f)
            {
                return angle <= 90f ? distance : -distance;
            }
            else
            {
                return distance;
            }
        }

        private float CalculateTurnInput(Vector3 toWaypoint)
        {
            Vector3 forward = Vector3.ProjectOnPlane(transform.parent.forward, Vector3.up).normalized;
            Vector3 direction = Vector3.ProjectOnPlane(toWaypoint, Vector3.up).normalized;

            float yawInput = Vector3.SignedAngle(forward, direction, Vector3.up);

            if (Mathf.Abs(yawInput) < 1f)
                return 0f;

            return Mathf.Sign(yawInput);
        }


        public void OnLook(GameObject target)
        {
            if (target == null) return;

            float yawDelta = CalculateYawDelta(target.transform.position, turret.position);
            float pitchDelta = CalculatePitchDelta(target.transform.position);

            lookInput = new Vector2(yawDelta, pitchDelta);

        }


        public float CalculateYawDelta(Vector3 tgtPosition, Vector3 turPosition)
        {
            // Calculate the direction to the target
            Vector3 toTarget = (tgtPosition - turPosition).normalized;

            // Get the vectors of the turret and the target
            // on a plane, ignoring the Y axis
            Vector3 turretFwd = Vector3.ProjectOnPlane(turret.forward, Vector3.up).normalized;
            Vector3 toTgt = Vector3.ProjectOnPlane(toTarget, Vector3.up).normalized;

            // Return the angle between from turretFwd to toTgt, 
            // negative if it's to the left and positive if it's to the right
            float yawDelta = Vector3.SignedAngle(turretFwd, toTgt, Vector3.up);

            return yawDelta;
        }

        // TODO: REFACTOR THIS FUCKING MESS, THIS IS LITERALLY JUST A THROW SHIT AT THE WALL AND SEE WHAT STICKS
        // Huge thanks to my brother for lending a helping hand with the mathematics in this method
        public float CalculatePitchDelta(Vector3 tgtPosition)
        {
            // Calculate distance between tank and target
            // It has to be the parent's position (The tank gameobject, not the _InternalScripts gameobject) if you want this to work and not fuck up
            float sqrDistance = (tgtPosition - transform.parent.position).sqrMagnitude;

            // actual distance in units
            float distance = Mathf.Sqrt(sqrDistance);

            // Formula to calculate the desired angle
            // Many thanks to my brother for this formula
            float targetAngle = -(5.9f * (30f * distance - 1000f) / 1916.47f);

            // I don't even know
            float currentAngle = muzzle.localEulerAngles.x;
            if (currentAngle > 180f) currentAngle -= 360f;

            float pitchDelta = -(targetAngle - currentAngle);

            return pitchDelta;
        }


        public void OnAttack1(bool fire)
        {
            fireMain = fire;
        }

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


        public float GetForwardInput() => movementInput.x;


        public float GetTurnInput() => movementInput.y;


        public Vector2 GetLookInput() => lookInput;


        public bool IsFiringMain() => fireMain;


        public bool IsFiringSec() => fireSecondary;
        
        
        public bool IsFiringTer() => fireTertiary;
    }
}
