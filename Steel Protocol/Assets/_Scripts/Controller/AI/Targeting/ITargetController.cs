using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.AI.Targeting
{
    public interface ITargetControl
    {
        Target CreateTarget(Transform target);
        void DestroyTarget(HashSet<Target> targets, Transform target);
    }
}
