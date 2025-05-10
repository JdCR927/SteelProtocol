using System.Collections.Generic;
using UnityEngine;
using SteelProtocol.Input;
using SteelProtocol.Controller.AI.Targeting;

namespace SteelProtocol.Controller.AI.Enemy
{
    

    public class AiEnemyTurretController : TargetController
    {
        private HashSet<Target> targets = new HashSet<Target>();
        private GameObject currentTarget;


        // Interface for input handling
        private AiInputBridge input;
        // Controller for aiming
        private TurretController aiming;

        // TODO: Debug stuff, remove later
        private SphereCollider detectionRange;

        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            aiming = GetComponent<TurretController>();

            if (input == null)
                Debug.LogError("Missing AiInputBridge component.");
            if (aiming == null)
                Debug.LogError("Missing TurretController component.");

            // TODO: Debug stuff, remove later
            detectionRange = GetComponent<SphereCollider>();
            if (detectionRange == null)
            {
                Debug.LogError("Sphere collider for the detection range not found on the tank.");
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            // Check if the object that entered the trigger is tagged as "Player" or "Friend"
            if (other.CompareTag("Player") || other.CompareTag("Friend"))
            {
                // Creates a target and adds it to the targets HashSet
                targets.Add(CreateTarget(other.transform));
            }
        }

        
        private void OnTriggerExit(Collider other)
        {
            // Check if the object that exited the trigger is tagged as "Player" or "Friend"
            if (other.CompareTag("Player") || other.CompareTag("Friend"))
            {
                // Destroys the target
                DestroyTarget(targets, other.transform);
            }
        }


        public void Update()
        {
            currentTarget = GetClosestTarget(targets);

            if (currentTarget == null) return;

            input.OnLook(currentTarget);

            aiming.Aim(input.GetLookInput());
        } 
    }
}
