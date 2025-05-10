using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.AI.Targeting
{
    public class TargetController : MonoBehaviour, ITargetControl
    {
        public Target CreateTarget(Transform target)
        {
            // Create a ray from this tank to the target
            Ray ray = new(transform.position, target.position - transform.position);

            // Create a raycast from the previous ray, and stores the hit information in the hit variable
            Physics.Raycast(ray, out RaycastHit hit);

            // Creates a new target
            Target newTarget = new(target.gameObject, ray, hit.distance);

            return newTarget;
        }


        public void DestroyTarget(HashSet<Target> targets, Transform target)
        {
            foreach (Target t in targets)
            {
                if (t.TargetObject == target.gameObject)
                {
                    // Removes the target from the HashSet
                    targets.Remove(t);
                    break;
                }
            }
        }
        
        
        // TODO: Raycast.distance doesn't update constantly, so the distance is not accurate
        // TODO: I don't even fucking know at this point
        // TODO: FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUCK
        public GameObject GetClosestTarget(HashSet<Target> targets)
        {
            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Target target in targets)
            {
                float distance = target.Distance;

                // Check if this target is closer than the previous closest target
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.TargetObject;
                }
            }

            return closestTarget;
        }

    }
}
