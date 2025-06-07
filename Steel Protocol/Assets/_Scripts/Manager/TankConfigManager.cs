using UnityEngine;
using SteelProtocol.Data.Armor;
using SteelProtocol.Data.Engine;
using SteelProtocol.Data.Shell;
using SteelProtocol.Data.Track;
using SteelProtocol.Data.Turret;
using SteelProtocol.Controller.Tank.Common.Weapons;
using SteelProtocol.Controller.Tank.Common.Turret;
using SteelProtocol.Controller.Tank.Common.Movement;
using SteelProtocol.Controller.Tank.Common.HP;

namespace SteelProtocol.Manager
{
    public class TankConfigManager : MonoBehaviour
    {
        private ArmorManager armorManager;
        private EngineManager engineManager;
        private ShellManager shellManager;
        private TrackManager trackManager;
        private TurretManager turretManager;

        public ArmorData CurrentArmorData { get; private set; }
        public EngineData CurrentEngineData { get; private set; }
        public ShellData CurrentShellData { get; private set; }
        public TrackData CurrentTrackData { get; private set; }
        public TurretData CurrentTurretData { get; private set; }


        private void Awake()
        {
            armorManager = GetComponent<ArmorManager>();
            engineManager = GetComponent<EngineManager>();
            shellManager = GetComponent<ShellManager>();
            trackManager = GetComponent<TrackManager>();
            turretManager = GetComponent<TurretManager>();



            if (armorManager != null)
                armorManager.LoadData();

            if (engineManager != null)
                engineManager.LoadData();

            if (shellManager != null)
                shellManager.LoadData();

            if (trackManager != null)
                trackManager.LoadData();

            if (turretManager != null)
                turretManager.LoadData();
        }

        public void SetArmorById(string id)
        {
            CurrentArmorData = armorManager.GetArmorById(id);

            var armor = GetComponentInChildren<HealthController>();

            if (CurrentArmorData != null && armor != null)
            {
                armor.Initialize(CurrentArmorData);
            }
        }

        public void SetEngineById(string id)
        {
            CurrentEngineData = engineManager.GetEngineById(id);

            var engine = GetComponentInChildren<MovementController>();

            if (CurrentEngineData != null && engine != null)
            {
                engine.InitializeEngine(CurrentEngineData);
            }
        }

        public void SetShellById(string id)
        {
            CurrentShellData = shellManager.GetShellById(id);

            // This is fucking retarded, fuck the load order in Unity
            var weapon = GetComponentInChildren<MainWeaponController>();

            if (CurrentShellData != null && weapon != null)
            {
                weapon.Initialize(CurrentShellData);
            }
        }

        public void SetTrackById(string id)
        {
            CurrentTrackData = trackManager.GetTrackById(id);

            var track = GetComponentInChildren<MovementController>();

            if (CurrentTrackData != null && track != null)
            {
                track.InitializeTrack(CurrentTrackData);
            }
        }

        public void SetTurretById(string id)
        {
            CurrentTurretData = turretManager.GetTurretById(id);

            var turret = GetComponentInChildren<TurretController>();

            if (CurrentTurretData != null && turret != null)
            {
                turret.Initialize(CurrentTurretData);
            }
        }

    }
}
