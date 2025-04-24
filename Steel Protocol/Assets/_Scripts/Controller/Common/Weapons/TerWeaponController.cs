using UnityEngine;

namespace SteelProtocol.Controller.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class TerWeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private Transform firePoint;

        [SerializeField] private float fireCooldown = 2f;

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

    //////////////////////////////////////
    // ToDo: Implement this when needed //
    //////////////////////////////////////
}