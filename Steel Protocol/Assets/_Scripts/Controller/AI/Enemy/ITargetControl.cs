using UnityEngine;

namespace SteelProtocol.Controller.AI
{
    public interface ITargetControl
    {
        
        void GetClosestTarget(Transform target);
        void SetTarget(Transform target);
        void SpawnRaycast(Transform target);
        void DestroyRaycast(Transform target);

    }
}
