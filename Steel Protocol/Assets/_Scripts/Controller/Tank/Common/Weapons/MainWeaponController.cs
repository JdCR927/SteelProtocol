using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Data.Enum;
using SteelProtocol.Data.Shell;
using SteelProtocol.Controller.Tank.Common.Audio;

namespace SteelProtocol.Controller.Tank.Common.Weapons
{
    [RequireComponent(typeof(WeaponController))]
    public class MainWeaponController : MonoBehaviour
    {
        private TankConfigManager configManager;

        private GameObject shellPrefab;
        private GameObject muzzleFlashPrefab;
        private string cannonSfx;

        private Transform firingPoint;


        public float CurrentAmmo { get; private set; }
        public float MaxAmmo { get; private set; }
        public float ReloadTime { get; private set; }
        private bool isReloading = false;
        private float reloadTimer = 0f;
        private float fireCooldown;
        private float cooldownTimer = 0f;
        private float shellVelocity;

        public Faction FiringFaction { get; private set; }


        public event System.Action<float, float> OnAmmoChanged;
        public event System.Action<float> OnReloadStarted;


        private AudioSource source;


        public void Awake()
        {
            configManager = GetComponentInParent<TankConfigManager>();

            // Get the fire point from TankController
            firingPoint = GetComponent<TankController>().FiringPoint;

            AssignFaction();
        }



        public void Initialize(ShellData data)
        {
            shellPrefab = Resources.Load<GameObject>($"Prefabs/Shells/{data.model}");
            muzzleFlashPrefab = Resources.Load<GameObject>($"Prefabs/Effects/{data.muzzleEffect}");
            cannonSfx = data.cannonSound;

            MaxAmmo = data.ammo;
            fireCooldown = data.cooldown;
            ReloadTime = data.reloadTime;
            shellVelocity = data.velocity;

            CurrentAmmo = MaxAmmo;
            OnAmmoChanged?.Invoke(CurrentAmmo, MaxAmmo); // Yes, I know this is not on ammo changed technically, but it's a hack to make it appear when initialized
        }


        private void Update()
        {
            // Ticks down the cooldown timer
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;

            if (CurrentAmmo <= 0 && !isReloading)
            {
                StartReload();
            }

            HandleReload();
        }


        private void AssignFaction()
        {
            string rootTag = transform.root.tag;

            FiringFaction = rootTag switch
            {
                "Player" => Faction.Player,
                "Enemy" => Faction.Enemy,
                _ => Faction.Friend
            };
        }


        public void TryFire()
        {
            if (isReloading || cooldownTimer > 0)
                return;


            // Play the muzzle flash effect before firing the shell
            InstantiateMuzzleFlash();

            // Instantiate the shell prefab
            InstantiateShell();

            // Play sound and set the cooldown timer
            PlaySfx();
            cooldownTimer = fireCooldown;

            // Decrease the current ammo, call the event for the UI
            CurrentAmmo--;
            OnAmmoChanged?.Invoke(CurrentAmmo, MaxAmmo);
        }

        private void InstantiateMuzzleFlash()
        {
            // Play the muzzle flash effect before firing the shell. Makes it a child of the firing point to keep it in place
            GameObject vfxMuzzleFlash = Instantiate(muzzleFlashPrefab, firingPoint.position, firingPoint.rotation, firingPoint);

            // Destroys the vfx prefab after 5 seconds to avoid memory leaks
            Destroy(vfxMuzzleFlash, 5f);
        }

        private void InstantiateShell()
        {
            // Instantiate the shell
            GameObject shell = Instantiate(shellPrefab, firingPoint.position, firingPoint.rotation);

            // Shells are weird as fuck and the spawn facing downwards for some reason
            // So manually flip them upwards
            shell.transform.Rotate(-90, 0, 0);

            // If shell has the shell component, initialize it with
            // the shell data and give it a faction
            if (shell.TryGetComponent<Shell>(out var shellScript))
            {
                shellScript.Initialize(configManager.CurrentShellData);
                shellScript.OriginTag = FiringFaction;
            }

            // If the shell has a rigidbody, add force to it
            if (shell.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(firingPoint.forward * shellVelocity);
            }
        }

        private void PlaySfx()
        {
            var pool = GetComponent<TankAudioPool>();
            source = pool?.GetSource((int)EnumAudioIndex.Cannon);
            
            var clip = AudioManager.Instance.GetSFXClip(cannonSfx);
            if (clip != null && source != null)
            {
                source.Stop(); // Optional: ensure it's not already playing
                source.clip = clip;
                source.loop = false;
                source.spatialBlend = 1f;
                source.rolloffMode = AudioRolloffMode.Logarithmic;
                source.minDistance = 20f;
                source.maxDistance = 300f;
                source.volume = AudioManager.Instance.SfxVolume; // Optional, to sync with settings

                source.Play();
            }
        }

        private void StartReload()
        {
            isReloading = true; // Set isReloading to true
            reloadTimer = 0f; // Set the reloadTimer to 0
        }

        private void HandleReload()
        {
            if (!isReloading) return;

            // Wait until the reloadTimer reaches the time to reload,
            // then refill the current ammo, set isReloading to false,
            // and finally call for the UI event
            reloadTimer += Time.deltaTime;
            OnReloadStarted?.Invoke(ReloadTime); // Call the event for the UI

            if (reloadTimer >= ReloadTime)
            {
                CurrentAmmo = MaxAmmo;
                isReloading = false;
                OnAmmoChanged?.Invoke(CurrentAmmo, MaxAmmo);
            }
        }

    }
}