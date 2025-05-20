using UnityEngine;
using SteelProtocol.Manager;

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
        armorPiercing,
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
        public TurretType turretId = TurretType.standardDrive;

        private TankConfigManager configManager;

        private void Start()
        {
            configManager = GetComponent<TankConfigManager>();

            configManager.SetArmorById(armorId.ToString());
            configManager.SetEngineById(engineId.ToString());
            // TODO: configManager.SetShellById(shellId.ToString());
            configManager.SetTurretById(turretId.ToString());
        }
    }

}
