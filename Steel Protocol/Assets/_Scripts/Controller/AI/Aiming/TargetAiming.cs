using System.Collections.Generic;
using SteelProtocol.Controller.AI;
using UnityEngine;

namespace SteelProtocol.Controller.AI.Aiming
{
    public struct Target
    {
        private GameObject target;
        private Ray raycast;
        private float distance;

        public Target(GameObject target, Ray raycast, float distance)
        {
            this.target = target;
            this.raycast = raycast;
            this.distance = distance;
        }

        public GameObject TargetObject { get => target; set => this.target = value; }

        public Ray Raycast { get => raycast; set => this.raycast = value; }

        public float Distance { get => distance; set => this.distance = value; }
    }

    public class TargetAiming : MonoBehaviour, ITargetControl
    {
        public Target CreateTarget(Transform target)
        {
            // Raycast's hit info
            RaycastHit hit;

            // Create a ray from this tank to the target
            Ray ray = new Ray(transform.position, target.position - transform.position);

            // Create a raycast from the previous ray, and stores the hit information in the hit variable
            Physics.Raycast(ray, out hit);

            // Creates a new target
            Target newTarget = new Target(target.gameObject, ray, hit.distance);

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

                    // Destroys the target's GameObject
                    Destroy(t.TargetObject);
                    break;
                }
            }
        }
        
        
        public void GetClosestTarget(HashSet<Target> targets)
        {
            // TODO
        }

        public Transform SetTarget(Transform target)
        {
            // TODO
        }
    }
}
