using UnityEngine;
using SteelProtocol.Data;
using SteelProtocol.Data.Shell;
using SteelProtocol.Controller.Tank.Common.HP;
using SteelProtocol.UI.HUD;


namespace SteelProtocol
{
    public class Shell : MonoBehaviour
    {
        private GameObject explosionPrefab;
        private float damage;

        private Rigidbody rb;

        public Faction OriginTag { get; set; }


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


        // This function is called when the shell collides with something
        private void OnCollisionEnter(Collision collision)
        {
            // Try to deal damage if target has a HealthController
            var health = collision.collider.GetComponentInChildren<HealthController>();

            if (health != null)
            {
                DamageChecker(collision, health);
            }

            CreateExplosion();

            // Destroy the shell after impact
            Destroy(gameObject);
        }


        private void CreateExplosion()
        {
            // Spawns explosion effect
            // Explosion effect credited to "Explosion" by "Gabriel Aguiar Prod." from "EASY EXPLOSIONS in Unity - Particle System vs VFX Graph" https://www.youtube.com/watch?v=adgeiUNlajY
            if (explosionPrefab != null)
            {
                GameObject vfxExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                // Destroys the explosion prefab after 8 seconds to avoid memory leaks
                Destroy(vfxExplosion, 8f);
            }
        }


        private void DamageChecker(Collision collision, HealthController health)
        {
            string targetTag = collision.collider.tag;

            if (CanDamage(OriginTag, targetTag))
            {
                health.TakeDamage(damage, OriginTag);
            }
            else
                health.TakeDamage(0); // Made so that the RigidBodies can be made kinematic when hit, otherwise they'll get pushed
        }

        private static bool CanDamage(Faction origin, string targetTag)
        {
            return origin switch
            {
                Faction.Player => targetTag == "Enemy",
                Faction.Friend => targetTag == "Enemy",
                Faction.Enemy => targetTag == "Player" || targetTag == "Friend",
                _ => false
            };
        }
        
    }
}