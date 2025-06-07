using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Turret;
using SteelProtocol.Controller.Tank.AI.Common.FCS;
using SteelProtocol.Controller.Tank.AI.Common.Targeting;

namespace SteelProtocol.Controller.Tank.AI.Enemy
{
    public class AiEnemyTargeting : DetectionTrigger
    {
        private AiInputBridge input;
        private TurretController aiming;
        private FiringControlSystem fcs;

        private bool attackFlag = false;

        private void Awake()
        {
            input = GetComponent<AiInputBridge>();
            aiming = GetComponent<TurretController>();
            fcs = GetComponent<FiringControlSystem>();
        }

        private void Update()
        {
            TickDetection();

            if (GetClosestTarget() is not GameObject target) return;

            input.OnLook(target);
            aiming.Aim(input.GetLookInput());

            attackFlag = Mathf.Abs(input.GetLookInput().y) <= 1f;
            fcs.FireWeapons(attackFlag);
        }

        protected override bool IsValidTarget(GameObject target)
        {
            return target.CompareTag("Player") || target.CompareTag("Friend");
        }
    }
}
