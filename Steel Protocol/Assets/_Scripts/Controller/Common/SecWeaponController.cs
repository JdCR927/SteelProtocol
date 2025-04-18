using UnityEngine;

namespace SteelProtocol.Controller.Common
{
    [RequireComponent(typeof(WeaponController))]
    public class SecWeaponController : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform firePoint;
        public float fireCooldown = 2f;

        private float cooldownTimer;

        private void Update()
        {
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
        }

        public void TryFire()
        {
            if (cooldownTimer > 0 || projectilePrefab == null || firePoint == null)
                return;

            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            cooldownTimer = fireCooldown;
        }
    }
}