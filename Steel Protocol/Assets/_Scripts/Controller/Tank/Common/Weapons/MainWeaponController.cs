using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Data.Shell;

namespace SteelProtocol.Controller.Tank.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class MainWeaponController : MonoBehaviour
    {
        private TankConfigManager configManager;

        private GameObject shellPrefab;
        private GameObject muzzleFlashPrefab;

        private Transform firingPoint;

        // TODO Ammo and reload time are not used yet
        //private float ammo;
        private float fireCooldown;
        //private float reloadTime;
        private float shellVelocity;


        // The cooldown timer for the weapon, initialized to 0
        private float cooldownTimer;


        public void Awake()
        {
            configManager = GetComponentInParent<TankConfigManager>();

            // Get the fire point from TankController
            firingPoint = GetComponent<TankController>().FiringPoint;
        }


        public void Initialize(ShellData data)
        {
            shellPrefab = Resources.Load<GameObject>($"Prefabs/Shells/{data.model}");
            muzzleFlashPrefab = Resources.Load<GameObject>($"Prefabs/Effects/{data.muzzleEffect}");

            // TODO Ammo and reload time are not used yet
            //ammo = data.ammo;
            fireCooldown = data.cooldown;
            //reloadTime = data.reloadTime;
            shellVelocity = data.velocity;
        }


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
            else if (shellPrefab == null || firingPoint == null)
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
            GameObject vfxMuzzleFlash = Instantiate(muzzleFlashPrefab, firingPoint.position, firingPoint.rotation, firingPoint);
            
            
            // Destroys the vfx prefab after 5 seconds to avoid memory leaks
            Destroy(vfxMuzzleFlash, 5f);
        }

        
        // This method instantiates the shell prefab at the fire point and applies force to it
        private void InstantiateShell()
        {
            // Instantiate the shell
            GameObject shell = Instantiate(shellPrefab, firingPoint.position, firingPoint.rotation);


            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // ToDo: Fucking fix the standard shell prefab, I'm 99% sure it literally only affects that prefab //
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // Fucking stupid piece of shit prefab won't rotate properly without this
            shell.transform.Rotate(-90, 0, 0);


            if (shell.TryGetComponent<Shell>(out var shellScript))
            {
                shellScript.Initialize(configManager.CurrentShellData);
            }


            if (shell.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(firingPoint.forward * shellVelocity);
            }
        }
    }
}