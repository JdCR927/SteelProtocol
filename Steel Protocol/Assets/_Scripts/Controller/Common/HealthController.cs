using UnityEngine;
using UnityEngine.Events;

namespace SteelProtocol.Controller.Common
{
    public class HealthController: MonoBehaviour
    {
        [Header("Tank Health")]
        public float health = 100f;
        private float armorMultiplier = 1f;
        private float currentHealth;

        public void Awake()
        {
            // Initialize health taking into account the type of armor
            currentHealth = health * armorMultiplier;
        }

        public void TakeDamage(float damage)
        {
            // Applies damage to the current health
            currentHealth -= damage;

            // Checks for death
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            // Destroy the game object
            // ToDo: Crude way to destroy the object, should be replaced with a more elegant solution
            Destroy(gameObject); 
        }
    }
}