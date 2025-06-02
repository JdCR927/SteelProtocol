using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Processing
{
    public class AiTurretProcessor
    {
        public float CalculateYawDelta(Vector3 turretPos, Vector3 turretForward, Vector3 targetPos)
        {
            Vector3 toTarget = (targetPos - turretPos).normalized;
            Vector3 tFwd = Vector3.ProjectOnPlane(turretForward, Vector3.up).normalized;
            Vector3 toTgt = Vector3.ProjectOnPlane(toTarget, Vector3.up).normalized;
            return Vector3.SignedAngle(tFwd, toTgt, Vector3.up);
        }

        public float CalculatePitchDelta(Vector3 rootPos, Vector3 muzzleLocalEulerX, Vector3 targetPos)
        {
            float dist = Vector3.Distance(rootPos, targetPos);

            // Formula to calculate the desired angle
            // Many thanks to my brother for this formula
            float targetAngle = -(6f * (30f * dist - 1000f) / 1916.47f);

            float currentAngle = muzzleLocalEulerX.x;
            if (currentAngle > 180f) currentAngle -= 360f;

            return -(targetAngle - currentAngle);
        }
    }
}
