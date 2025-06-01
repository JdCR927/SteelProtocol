using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Processing
{
    public class AiMovementProcessor
    {
        public float CalculateForwardInput(Vector3 fromPos, Vector3 toPos, Vector3 forward)
        {
            Vector3 dir = Vector3.ProjectOnPlane(toPos - fromPos, Vector3.up).normalized;
            Vector3 fwd = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;

            float angle = Vector3.Angle(fwd, dir);
            float distance = Vector3.Distance(fromPos, toPos);

            float brakingDistance = distance / 20;

            return distance <= 50f ? (angle <= 90f ? brakingDistance : -brakingDistance) : brakingDistance;
        }

        public float CalculateTurnInput(Vector3 fromPos, Vector3 toPos, Vector3 forward)
        {
            Vector3 dir = Vector3.ProjectOnPlane(toPos - fromPos, Vector3.up).normalized;
            Vector3 fwd = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;

            float yaw = Vector3.SignedAngle(fwd, dir, Vector3.up);
            return Mathf.Abs(yaw) < 1f ? 0f : Mathf.Sign(yaw);
        }
    }
}