using UnityEngine;
using SteelProtocol.Manager;
using SteelProtocol.Data.Enum;
using SteelProtocol.Controller.Tank.Common.HP;
using SteelProtocol.Controller.Tank.Common.Turret;
using SteelProtocol.Controller.Tank.Common.Weapons;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Loadout
{
    public class Loadout : MonoBehaviour
    {
        public EnumArmor armorId = EnumArmor.standardArmor;
        public EnumEngine engineId = EnumEngine.standardEngine;
        public EnumShell shellId = EnumShell.armorPiercing;
        public EnumTrack trackId = EnumTrack.standardTracks;
        public EnumTurret turretId = EnumTurret.standardDrive;

        private TankConfigManager configManager;

        private void Start()
        {
            configManager = GetComponent<TankConfigManager>();

            if (GetComponentInChildren<HealthController>() != null)
                configManager.SetArmorById(armorId.ToString());

            if (GetComponentInChildren<MovementController>() != null)
                configManager.SetEngineById(engineId.ToString());

            if (GetComponentInChildren<MainWeaponController>() != null)
                configManager.SetShellById(shellId.ToString());

            if (GetComponentInChildren<MovementController>() != null)
                configManager.SetTrackById(trackId.ToString());

            if (GetComponentInChildren<TurretController>() != null)
                configManager.SetTurretById(turretId.ToString());
        }
    }

}
