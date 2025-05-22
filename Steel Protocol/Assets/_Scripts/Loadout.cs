using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Controller.Tank.Common.HP;
using SteelProtocol.Controller.Tank.Common.Movement;
using SteelProtocol.Controller.Tank.Common.Weapons;
using SteelProtocol.Controller.Tank.Common.Turret;

namespace SteelProtocol
{
    // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
    public enum ArmorType
    {
        normalArmor,
        mediumArmor,
        lightArmor
    }

// TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
    public enum EngineType
    {
        normalEngine,
        engineMk2,
        accelerator
    }

// TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
    public enum ShellType
    {
        armorPiercing
    }


    // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
    public enum TrackType
    {
        normalTracks,
        fastTracks,
        highGripTracks
    }

    // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
    public enum TurretType
    {
        standardDrive,
        standardDriveMk2,
        heavyDrive,
        precisionDrive
    }

    public class Loadout : MonoBehaviour
    {
        // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
        public ArmorType armorId = ArmorType.normalArmor;
        // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
        public EngineType engineId = EngineType.normalEngine;
        // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
        public ShellType shellId = ShellType.armorPiercing;
        // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
        public TrackType trackId = TrackType.normalTracks;
        // TODO: THIS SHIT IS JUST FOR TESTING, REMOVE
        public TurretType turretId = TurretType.standardDrive;

        private TankConfigManager configManager;

        private void Start()
        {
            configManager = GetComponent<TankConfigManager>();

            if (GetComponentInChildren<HealthController>() != null)
                configManager.SetArmorById(armorId.ToString());
            else
                Debug.LogWarning(name + ": HealthController not found, skipping armor initialization.");

            if (GetComponentInChildren<MovementController>() != null)
                configManager.SetEngineById(engineId.ToString());
            else
                Debug.LogWarning(name + ": MovementController not found, skipping engine initialization.");

            if (GetComponentInChildren<MainWeaponController>() != null)
                configManager.SetShellById(shellId.ToString());
            else
                Debug.LogWarning(name + ": MainWeaponController not found, skipping shell initialization.");

            if (GetComponentInChildren<MovementController>() != null)
                configManager.SetTrackById(trackId.ToString());
            else
                Debug.LogWarning(name + ": MovementController not found, skipping track initialization.");

            if (GetComponentInChildren<TurretController>() != null)
                configManager.SetTurretById(turretId.ToString());
            else
                Debug.LogWarning(name + ": TurretController not found, skipping turret initialization.");
        }
    }

}
