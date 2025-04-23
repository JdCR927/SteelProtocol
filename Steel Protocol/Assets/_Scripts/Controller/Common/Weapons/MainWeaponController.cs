using SteelProtocol.Manager;
using UnityEngine;

namespace SteelProtocol.Controller.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class MainWeaponController : MonoBehaviour
    {
        public GameObject shellPrefab;
        public GameObject muzzleFlashPrefab;
        public Transform firePoint;
        public float fireCooldown = 2f;

        private float cooldownTimer;

        private void Update()
        {
            // Ticks down the cooldown timer
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
        }

        public void TryFire()
        {
            // Checks if either: The cooldown timer is still active, the shell prefab is not assigned, or the fire point is not assigned
            if (cooldownTimer > 0)
            {
                return;
            }
            else if (shellPrefab == null || firePoint == null)
            {
                Debug.LogError("Shell prefab or fire point not assigned in MainWeaponController.");
                return;
            }

            InstantiateMuzzleFlash(); // Play the muzzle flash effect before firing the shell


            InstantiateShell();

            // Play sound and reset cooldown
            AudioManager.Instance.PlaySFX("Cannon", 1f);
            cooldownTimer = fireCooldown;
        }

        private void InstantiateMuzzleFlash()
        {
            // Play the muzzle flash effect before firing the shell. Makes it a child of the firing point to keep it in place
            GameObject vfxMuzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);
            Destroy(vfxMuzzleFlash, 5f); // Destroys the vfx prefab after 5 seconds to avoid memory leaks
        }

        private void InstantiateShell()
        {
            // Instantiate the shell
            GameObject shell = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);

            // Fucking stupid piece of shit prefab won't rotate properly without this
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // ToDo: Fucking fix the standard shell prefab, I'm 99% sure it literally only affects that prefab //
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            shell.transform.Rotate(-90, 0, 0);

            // Add force to shoot it forward
            // Also, not gonna lie to you chief, the IDE did whatever this If condition is. Thank it, because I have no idea exactly what it does
            if (shell.TryGetComponent<Rigidbody>(out var rb))
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // ToDo: Change the hardcoded value for a variable. Force/Speed will be extracted from... somewhere (JSON maybe? Structs I guess?) //
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                float shootForce = 3000f;
                rb.AddForce(firePoint.forward * shootForce);
            }
        }
    }
}