using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.AI.Targeting
{
    public abstract class TargetController : MonoBehaviour, ITargetControl
    {
        public Target CreateTarget(Transform target)
        {
            // Creates and returns new target
            return new Target (target.gameObject);
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
    }
}
