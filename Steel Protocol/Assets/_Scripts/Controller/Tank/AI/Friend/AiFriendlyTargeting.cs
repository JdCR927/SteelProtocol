using UnityEngine;
using SteelProtocol.Controller.Tank.AI.FCS;
using SteelProtocol.Controller.Tank.AI.Targeting;
using SteelProtocol.Controller.Tank.Common.Turret;

namespace SteelProtocol.Controller.Tank.AI.Friend
{
    public class AiFriendlyTargeting : DetectionTrigger
    {
        // Interface for input handling
        private AiInputBridge input;

        private TurretController aiming;

        private FiringControlSystem fcs;

        private bool attackFlag = false;


        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            aiming = GetComponent<TurretController>();
            fcs = GetComponent<FiringControlSystem>();
        }


        public void Update()
        {
            // Check for the closest target
            currentTarget = GetClosestTarget(targets);

            // If no target is found, return
            if (currentTarget == null) return;

            // Update the input with the current target
            input.OnLook(currentTarget);
                        
            // Aim at the target
            aiming.Aim(input.GetLookInput());

            attackFlag = Mathf.Abs(input.GetLookInput().y) <= 1f;

            fcs.FireWeapons(attackFlag);
        } 

        
        protected override bool ShouldRegisterTarget(Collider other)
        {
            return other.CompareTag("Enemy");
        }
    }
}
