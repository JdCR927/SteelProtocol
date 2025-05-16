using UnityEngine;
using System.Collections.Generic;

namespace SteelProtocol.Controller.AI.Targeting
{
    public abstract class DetectionTrigger : TargetController
    {
        protected HashSet<Target> targets = new HashSet<Target>();
        protected GameObject currentTarget;


        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider is a trigger
            if (other.isTrigger) return;

            // If it is, check if it is the type of target we want to register
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
                // I've been scouring the internet to find a way to get the distance between two objects
                // and they recommended using the sqrMagnitude method for optimization
                // Thanks to the very helpful special "chapi" for helping with coding this
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


        // Just visibility methods, to clean up the code
        public void RegisterTarget(Transform target) => targets.Add(CreateTarget(target));
        public void UnregisterTarget(Transform target) => DestroyTarget(targets, target);
    }
}
