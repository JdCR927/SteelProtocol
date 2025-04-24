using SteelProtocol.Manager;
using UnityEngine;

namespace SteelProtocol.Controller.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class MainWeaponController : MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ToDo: Investigate a way to get the shell prefab without manually dragging it in the inspector //
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // The prefab for the shell that will be fired
        [SerializeField] private GameObject shellPrefab;

        // The prefab for the muzzle flash effect
        [SerializeField] private GameObject muzzleFlashPrefab;

        // The point from which the shell will be fired
        // This should be a child of the muzzle/cannon
        [SerializeField] private Transform firePoint;
        
        /////////////////////////////////////////////////////////////////////
        // ToDo: Get the cooldown from somewhere else, just like the force //
        /////////////////////////////////////////////////////////////////////
        // The cooldown time between shots, in seconds
        [SerializeField] private float fireCooldown = 2f;

        // The cooldown timer for the weapon, initialized to 0
        private float cooldownTimer;


        private void Update()
        {
            // Ticks down the cooldown timer
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
        }


        // This method is called to fire the weapon.
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


            // Play the muzzle flash effect before firing the shell
            InstantiateMuzzleFlash(); 


            // Instantiate the shell prefab
            InstantiateShell();


            // Play sound and reset cooldown
            AudioManager.Instance.PlaySFX("Cannon", 1f);
            cooldownTimer = fireCooldown;
        }

        
        // This method instantiates the muzzle flash prefab at the fire point and destroys it after 5 seconds to avoid memory leaks
        private void InstantiateMuzzleFlash()
        {
            // Play the muzzle flash effect before firing the shell. Makes it a child of the firing point to keep it in place
            GameObject vfxMuzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);
            
            
            // Destroys the vfx prefab after 5 seconds to avoid memory leaks
            Destroy(vfxMuzzleFlash, 5f);
        }

        
        // This method instantiates the shell prefab at the fire point and applies force to it
        private void InstantiateShell()
        {
            // Instantiate the shell
            GameObject shell = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);


            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // ToDo: Fucking fix the standard shell prefab, I'm 99% sure it literally only affects that prefab //
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // Fucking stupid piece of shit prefab won't rotate properly without this
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