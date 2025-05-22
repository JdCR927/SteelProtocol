using UnityEngine;

namespace SteelProtocol.Controller.Tank.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class SecWeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private Transform firePoint;

        // [SerializeField] private float fireCooldown = 2f;

        private float cooldownTimer;

        private void Update()
        {
            // ToDo: Implement
        }

        public void TryFire()
        {
            // ToDo: Implement
        }
    }
}