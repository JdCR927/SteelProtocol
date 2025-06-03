using UnityEngine;
using SteelProtocol.Data.Armor;
using System;
using SteelProtocol.Data;

namespace SteelProtocol.Controller.Tank.Common.HP
{
    public class HealthController: MonoBehaviour
    {
        protected float maxHealth;
        protected float currentHealth;

        public float MaxHealth { get; set; }
        public float CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = Mathf.Clamp(value, 0.01f, maxHealth);
                OnHealthChanged?.Invoke(currentHealth / maxHealth * 100f);
            }
        }

        public event Action<float> OnHealthChanged;


        public void Initialize(ArmorData data)
        {
            maxHealth = data.health;
            currentHealth = maxHealth;
        }


        public virtual void TakeDamage(float damage)
        {
            // Makes the tank kinematic to avoid physics interactions while applying damage
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;

            // Applies damage to the current health
            if (damage > 0)
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
        
        public virtual void TakeDamage(float damage, Faction sourceFaction)
        {
            TakeDamage(damage); // Default fallback
        }


        // Method called when the tank runs out of health
        public void Die()
        {
            // Destroys the game object
            Destroy(transform.root.gameObject);
        }
    }
}