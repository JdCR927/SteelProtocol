using UnityEngine;
using UnityEngine.Events;

namespace SteelProtocol.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class HealthController: MonoBehaviour
    {
        // Tank's health max and initial health
        [Header("Tank Health")]
        [SerializeField]private float health = 100f;
        
        ///////////////////////////////////////////////////////////////////////
        // ToDo: Get the armor multiplier from somewhere else, not hardcoded //
        ///////////////////////////////////////////////////////////////////////
        // Tank's armor multiplier
        private float armorMultiplier = 1f;

        // Tank's current health
        private float currentHealth;


        public void Awake()
        {
            // Initialize health taking into account the type of armor
            currentHealth = health * armorMultiplier;
        }


        // Method to apply damage to the tank's health
        public void TakeDamage(float damage)
        {
            // Makes the tank kinematic to avoid physics interactions while applying damage
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Applies damage to the current health
            currentHealth -= damage;

            // Makes the tank non-kinematic again to allow physics interactions
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            // Checks for death
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }


        // Method called when the tank runs out of health
        public void Die()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////
            // ToDo: Crude way to destroy the object, should be replaced with a more elegant solution //
            ////////////////////////////////////////////////////////////////////////////////////////////
            // Destroys the game object
            Destroy(gameObject); 
        }
    }
}