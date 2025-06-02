using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Targeting
{
    public interface ITargetControl
    {
        Target CreateTarget(Transform target);
        void DestroyTarget(HashSet<Target> targets, Transform target);
    }
}
