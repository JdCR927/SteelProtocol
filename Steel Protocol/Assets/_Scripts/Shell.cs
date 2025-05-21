using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Data.Shell;
using SteelProtocol.Controller.Tank.Common.HP;


namespace SteelProtocol
{
    public class Shell : MonoBehaviour
    {
        [HideInInspector] public GameObject explosionPrefab;
        [HideInInspector] public float damage = 20f;


        private Rigidbody rb;


        private void Start()
        {
            // Get the Rigidbody component attached to the shell
            rb = GetComponent<Rigidbody>();
        }


        public void Initialize(ShellData data)
        {
            damage = data.damage;

            explosionPrefab = Resources.Load<GameObject>($"Prefabs/Effects/{data.explosionEffect}");
        }


        private void FixedUpdate()
        {
            // Rotates the shell to face the direction of its velocity instead of being locked to the initial rotation
            // Essentially it makes the shell rotate in the arc it has been fired in
            if (rb.linearVelocity.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f * Time.fixedDeltaTime);
            }
        }


        ///////////////////////////////////////////////////////////
        // ToDo: Maybe check out layers for collision detection? //
        ///////////////////////////////////////////////////////////
        // This function is called when the shell collides with something
        private void OnCollisionEnter(Collision collision)
        {
            // Try to deal damage if target has a HealthController
            var health = collision.collider.GetComponent<HealthController>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Spawns explosion effect
            // Explosion effect credited to "Explosion" by "Gabriel Aguiar Prod." from "EASY EXPLOSIONS in Unity - Particle System vs VFX Graph" https://www.youtube.com/watch?v=adgeiUNlajY
            if (explosionPrefab != null)
            {
                GameObject vfxExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                
                // Destroys the explosion prefab after 8 seconds to avoid memory leaks
                Destroy(vfxExplosion, 8f);
            }

            // Destroy the shell after impact
            Destroy(gameObject);
        }
        
    }
}