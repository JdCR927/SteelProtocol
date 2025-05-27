using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Data.Enum;
using SteelProtocol.Controller.Tank.Common.HP;
using SteelProtocol.Controller.Tank.Common.Movement;
using SteelProtocol.Controller.Tank.Common.Weapons;
using SteelProtocol.Controller.Tank.Common.Turret;

namespace SteelProtocol.Loadout
{
    public class Loadout : MonoBehaviour
    {
        public EnumArmor armorId = EnumArmor.normalArmor;
        public EnumEngine engineId = EnumEngine.normalEngine;
        public EnumShell shellId = EnumShell.armorPiercing;
        public EnumTrack trackId = EnumTrack.normalTracks;
        public EnumTurret turretId = EnumTurret.standardDrive;

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
