using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Targeting
{
    public abstract class DetectionTrigger : MonoBehaviour
    {
        private readonly float detectionRadius = 200f * 200f;
        [SerializeField] protected LayerMask targetMask;

        private GameObject currentTarget;
        private readonly Collider[] detectionBuffer = new Collider[32]; // Pre-allocated buffer to avoid Garbage Collector

        private readonly float updateInterval = 0.2f;
        private float nextUpdateTime = 0f;
        private float tickOffset;


        protected virtual void Start()
        {
            // Offset polling to spread CPU load
            tickOffset = Random.Range(0f, 0.1f);
        }

        protected void TickDetection()
        {
            if (Time.time < nextUpdateTime + tickOffset)
                return;

            UpdateClosestTarget();
            nextUpdateTime = Time.time + updateInterval + tickOffset;
        }

        private void UpdateClosestTarget()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectionBuffer, targetMask);

            GameObject closest = null;
            float closestSqrDist = Mathf.Infinity;
            Vector3 selfPos = transform.position;

            for (int i = 0; i < count; i++)
            {
                var col = detectionBuffer[i];
                if (col == null) continue;

                GameObject candidate = col.attachedRigidbody ? col.attachedRigidbody.gameObject : col.gameObject;

                if (!IsValidTarget(candidate)) continue;

                float sqrDist = (candidate.transform.position - selfPos).sqrMagnitude;

                if (sqrDist < closestSqrDist)
                {
                    closest = candidate;
                    closestSqrDist = sqrDist;
                }
            }

            currentTarget = closest;
        }

        public GameObject GetClosestTarget()
        {
            return currentTarget;
        }


        protected abstract bool IsValidTarget(GameObject target);
    }
}
