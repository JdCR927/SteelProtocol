using System.Collections.Generic;
using UnityEngine;
using SteelProtocol.Controller.AI.Aiming;

namespace SteelProtocol.Controller.AI
{
    public interface ITargetControl
    {
        
        Target CreateTarget(Transform target);
        void DestroyTarget(HashSet<Target> targets, Transform target);
        void GetClosestTarget(HashSet<Target> targets);
        Transform SetTarget(Transform target);

    }
}
