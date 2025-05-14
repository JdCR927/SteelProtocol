using UnityEngine;

namespace SteelProtocol.Controller.AI.Friendly
{
    public class AiFriendlyTurretController : DetectionTrigger
    {
        // Interface for input handling
        private AiInputBridge input;

        // Controller for aiming
        private TurretController aiming;


        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            aiming = GetComponent<TurretController>();

            if (input == null)
                Debug.LogError("Missing AiInputBridge component.");
            if (aiming == null)
                Debug.LogError("Missing TurretController component.");
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
        } 

        
        protected override bool ShouldRegisterTarget(Collider other)
        {
            // Return true if the object is tagged as "Enemy"
            return other.CompareTag("Enemy");
        }
    }
}
