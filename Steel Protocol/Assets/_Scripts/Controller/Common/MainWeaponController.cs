using UnityEngine;

namespace SteelProtocol.Controller.Common
{
    [RequireComponent(typeof(WeaponController))]
    public class MainWeaponController : MonoBehaviour
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

            if (cooldownTimer > 0)
                return;

            Debug.DrawRay(firePoint.position, firePoint.forward * 10000f, Color.red, 10f); //ToDo: Only shows in the Scene tab. Fucking retarded. Unity, pls fix.

            cooldownTimer = fireCooldown;
        }
    }
}