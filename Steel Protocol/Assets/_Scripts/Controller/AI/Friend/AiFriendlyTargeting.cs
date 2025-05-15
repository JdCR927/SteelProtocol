using SteelProtocol.Controller.AI.Targeting;
using SteelProtocol.Controller.AI.FCS;
using UnityEngine;

namespace SteelProtocol.Controller.AI.Friend
{
    public class AiFriendlyTargeting : DetectionTrigger
    {
        // Interface for input handling
        private AiInputBridge input;

        // Controller for aiming
        private TurretController aiming;

        private FiringControlSystem fcs;

        private bool attackFlag = false;


        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            aiming = GetComponent<TurretController>();
            fcs = GetComponent<FiringControlSystem>();

            if (input == null)
                Debug.LogError("Missing AiInputBridge component.");

            if (aiming == null)
                Debug.LogError("Missing TurretController component.");

            if (fcs == null)
                Debug.LogError("Missing FiringControlSystem component.");
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
            // Return true if the object is tagged as "Enemy"
            return other.CompareTag("Enemy");
        }
    }
}
