using SteelProtocol.Controller.Tank.AI.Common.FCS;
using SteelProtocol.Controller.Tank.AI.Common.Movement;
using SteelProtocol.Controller.Tank.AI.Common.Targeting;
using SteelProtocol.Controller.Tank.Common.Movement;
using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class StanceManager : MonoBehaviour
    {
        private AiStance currentStance;
        [SerializeField] private EnumStancesPrimary primaryStance;
        [SerializeField] private EnumStancesSecondary secondaryStance;

        [SerializeField] private Vector3[] waypoints;

        private AiMover mover;

        private void Awake()
        {
            mover = GetComponentInChildren<AiMover>();

            mover.SetWaypoints(waypoints);

            SetStanceFromEnum(primaryStance);
        }

        public void SetStance(AiStance newStance)
        {
            if (currentStance != null)
                currentStance.OnStanceExit();

            currentStance = newStance;
            currentStance.OnStanceEnter();
        }

        private void Update()
        {
            if (currentStance != null)
                currentStance.OnStanceUpdate();
        }

        private void SetStanceFromEnum(EnumStancesPrimary stance)
        {
            var input = GetComponentInChildren<AiInputBridge>();
            var movement = GetComponentInChildren<MovementController>();
            var fcs = GetComponentInChildren<FiringControlSystem>();
            var detection = FindDetectionTrigger();

            AiStance stanceComponent = stance switch
            {
                EnumStancesPrimary.PatrolStance => new PatrolStance(),
                EnumStancesPrimary.AggroStance => new AggroStance(),
                EnumStancesPrimary.HoldStance => new HoldStance(),
                _ => null
            };

            stanceComponent?.Initialize(input, movement, fcs, mover, detection);

            SetStance(stanceComponent);
        }
        
        private DetectionTrigger FindDetectionTrigger()
        {
            foreach (var mono in GetComponentsInChildren<MonoBehaviour>())
            {
                if (mono is DetectionTrigger trigger)
                    return trigger;
            }
            
            return null;
        }
    }
}
