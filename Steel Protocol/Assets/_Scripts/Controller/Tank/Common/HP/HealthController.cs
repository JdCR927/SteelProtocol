using UnityEngine;
using SteelProtocol.Data.Armor;
using System;

namespace SteelProtocol.Controller.Tank.Common.HP
{
    public class HealthController: MonoBehaviour
    {
        private float maxHealth;

        public event Action<float> OnHealthChanged;
        private float currentHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = Mathf.Clamp(value, 0.01f, maxHealth);
                OnHealthChanged?.Invoke((currentHealth / maxHealth) * 100f);
            }
        }


        public void Initialize(ArmorData data)
        {
            maxHealth = data.health;
            currentHealth = maxHealth;
        }


        public void TakeDamage(float damage)
        {
            // Makes the tank kinematic to avoid physics interactions while applying damage
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;

            // Applies damage to the current health
            CurrentHealth -= damage;

            // Makes the tank non-kinematic again to allow physics interactions
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;

            // Checks for death
            if (currentHealth <= 0.01f)
            {
                currentHealth = 0.01f;
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