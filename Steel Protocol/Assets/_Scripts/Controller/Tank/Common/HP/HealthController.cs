using UnityEngine;
using SteelProtocol.Data.Armor;
using SteelProtocol.Manager;

namespace SteelProtocol.Controller.Tank.Common.HP
{
    public class HealthController: MonoBehaviour
    {
        public TankConfigManager configManager;


        [HideInInspector]
        public float health;
        private float currentHealth;


        public void Awake()
        {
            configManager = GetComponentInParent<TankConfigManager>();
        }


        public void Start()
        {
            // Initialize the health based on the current armor data
            var data = configManager.CurrentArmorData;
            if (data != null)
                Initialize(data);
        }


        public void Initialize(ArmorData data)
        {
            // Sets the health to the maximum value defined in the armor data
            health = data.health;
            currentHealth = health;
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