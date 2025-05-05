using System.Collections.Generic;
using UnityEngine;

namespace SteelProtocol.Controller.AI
{
    public class AiEnemyTurretController : MonoBehaviour
    {
        
        private SphereCollider detectionRange;
        private HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

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
            if (other.CompareTag("Player") || other.CompareTag("Friend"))
            {
                enemiesInRange.Add(other.gameObject);
                Debug.Log("Enemy detected: " + other.gameObject.name);
            }
        }

        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Friend"))
            {
                enemiesInRange.Remove(other.gameObject);
                Debug.Log("Enemy left: " + other.gameObject.name);
            }
        }

        public void Update()
        {
            
        }

    }
}
