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
            currentHealth = health * armorMultiplier;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("This tank has died");
        }
    }
}