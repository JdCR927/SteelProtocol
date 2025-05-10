using System.Collections.Generic;
using UnityEngine;
using SteelProtocol.Controller.AI.Aiming;

namespace SteelProtocol.Controller.AI.Enemy
{
    

    public class AiEnemyTurretController : TargetAiming
    {
        private HashSet<Target> targets = new HashSet<Target>();
        private Transform currentTarget;

        // TODO: Debug stuff, remove later
        private SphereCollider detectionRange;

        public void Awake()
        {
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
            // TODO: Implement the logic to check if the target is in range and if it is, set it as the target
        }


        

        
    }
}
