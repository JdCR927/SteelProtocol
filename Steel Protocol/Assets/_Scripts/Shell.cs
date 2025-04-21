using UnityEngine;
using SteelProtocol.Controller.Common;

namespace SteelProtocol
{
    public class Shell : MonoBehaviour
    {
        public GameObject explosionPrefab;
        public float damage = 20f;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Rotates the shell to face the direction of its velocity instead of being locked to the initial rotation
            if (rb.linearVelocity.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f * Time.fixedDeltaTime);

            }
        }

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
                Destroy(vfxExplosion, 8f); // Destroys the explosion prefab after 8 seconds to avoid memory leaks
            }

            // Destroy the shell after impact
            Destroy(gameObject);
        }
    }
}