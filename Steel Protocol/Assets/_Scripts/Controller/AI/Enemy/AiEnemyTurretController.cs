using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.AI
{
    public class AiEnemyTurretController : MonoBehaviour, ITargetControl
    {
        
        private SphereCollider detectionRange;
        private Dictionary<GameObject, float> enemiesInRange = new Dictionary<GameObject, float>();
        private Dictionary<GameObject, Ray> raycasts = new Dictionary<GameObject, Ray>();
        [SerializeField] private Transform turret;

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
                // Spawn a raycast
                SpawnRaycast(other.transform);
            }
        }

        
        private void OnTriggerExit(Collider other)
        {
            // Check if the object that exited the trigger is tagged as "Player" or "Friend"
            if (other.CompareTag("Player") || other.CompareTag("Friend"))
            {
                // Destroy the raycast
                DestroyRaycast(other.transform);
            }
        }


        public void Update()
        {
            // TODO: Implement the logic to check if the target is in range and if it is, set it as the target
        }


        public void SpawnRaycast(Transform target)
        {
            // Raycast's hit info
            RaycastHit hit;

            // Create a ray from the turret to the target's position
            Ray ray = new Ray(turret.position, target.position - transform.position);

            // Create a raycast from the previous ray, and stores the hit information in the hit variable
            Physics.Raycast(ray, out hit);

            // Add the target and it's distance to the Target Dictionary, then add the raycast to the Raycast Dictionary
            AddTargetToDictionary(target.gameObject, hit.distance);
            AddRaycastToDictionary(target.gameObject, ray);

        }


        public void DestroyRaycast(Transform target)
        {
            // Destroy the target and the raycast from their respective dictionaries
            DestroyTargetInDictionary(target.gameObject);
            DestroyRaycastInDictionary(target.gameObject);
        }


        public void AddTargetToDictionary(GameObject target, float distance)
        {
            enemiesInRange.Add(target, distance);
        }


        public void AddRaycastToDictionary(GameObject target, Ray ray)
        {
            raycasts.Add(target, ray);
        }


        public void DestroyTargetInDictionary(GameObject target)
        {
            if (enemiesInRange.ContainsKey(target))
            {
                enemiesInRange.Remove(target);
            }
        }


        public void DestroyRaycastInDictionary(GameObject target)
        {
            if (raycasts.ContainsKey(target))
            {
                raycasts.Remove(target);
            }
        }
        
    
        public void GetClosestTarget(Transform target)
        {
            // TODO
        }

        public void SetTarget(Transform target)
        {
            // TODO
        }

        
    }
}
