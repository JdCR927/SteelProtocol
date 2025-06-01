using UnityEngine;
using System.Collections.Generic;

namespace SteelProtocol.Controller.Tank.AI.Targeting
{
    public abstract class DetectionTrigger : TargetController
    {
        protected HashSet<Target> targets = new HashSet<Target>();
        protected GameObject currentTarget;
        
        protected float closestTargetUpdateInterval = 0.2f;
        protected float targetCleanupInterval = 1.5f;

        private float closestTargetTimer = 0f;
        private float cleanupTimer = 0f;


        // Slows down the checks for finding the closest targets to help with performance
        // Also cleans nulls after x amount of time to clean the memory of any pesky cunts like that
        private void Update()
        {
            closestTargetTimer += Time.deltaTime;
            cleanupTimer += Time.deltaTime;

            if (closestTargetTimer >= closestTargetUpdateInterval)
            {
                currentTarget = GetClosestTarget(targets);
                closestTargetTimer = 0f;
            }

            if (cleanupTimer >= targetCleanupInterval)
            {
                CleanupInvalidTargets();
                cleanupTimer = 0f;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider is a trigger
            if (other.isTrigger) return;

            // If it isn't, check if it is the type of target we want to register
            if (ShouldRegisterTarget(other))
            {
                // If it is, create a new target and add it to the targets HashSet
                RegisterTarget(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the collider is a trigger
            if (other.isTrigger) return;

            // If it is, check if it is the type of target we want to unregister
            if (ShouldRegisterTarget(other))
            {
                // If it is, destroy the target and remove it from the targets HashSet
                UnregisterTarget(other.transform);
            }
        }

        protected abstract bool ShouldRegisterTarget(Collider other);


        public GameObject GetClosestTarget(HashSet<Target> targets)
        {
            // If there are no targets, return null
            if (targets == null || targets.Count == 0)
                return null;

            // Initialize variables to track the closest target
            // and the closest distance
            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Target target in targets)
            {
                // Check if the target is null or not
                // If it is null, skip to the next target
                if (target.TargetObject == null) continue;

                // Calculate the distance from this tank to the target
                float distance = (target.TargetObject.transform.position - transform.position).sqrMagnitude;

                // Check if this target is closer than the previous closest target
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.TargetObject;
                }
            }

            return closestTarget;
        }


        private void CleanupInvalidTargets()
        {
            targets.RemoveWhere(t => t.TargetObject == null);
        }

        // Just visibility methods, to clean up the code
        public void RegisterTarget(Transform target) => targets.Add(CreateTarget(target));
        public void UnregisterTarget(Transform target) => DestroyTarget(targets, target);
    }
}
