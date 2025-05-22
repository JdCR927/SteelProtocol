using UnityEngine;
using SteelProtocol.Data.Armor;

namespace SteelProtocol.Controller.Tank.Common.HP
{
    public class HealthController: MonoBehaviour
    {
        private float health;
        private float currentHealth;


        public void Initialize(ArmorData data)
        {
            health = data.health;
            currentHealth = health;
        }


        // Method to apply damage to the tank's health
        public void TakeDamage(float damage)
        {
            // Makes the tank kinematic to avoid physics interactions while applying damage
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;

            // Applies damage to the current health
            currentHealth -= damage;

            // Makes the tank non-kinematic again to allow physics interactions
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;

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
            Destroy(transform.root.gameObject); 
        }
    }
}