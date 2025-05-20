using UnityEngine;
using SteelProtocol.Data.Armor;
using SteelProtocol.Data.Engine;
using SteelProtocol.Data.Shell;
using SteelProtocol.Data.Track;
using SteelProtocol.Data.Turret;

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

            armorManager.LoadData();
            engineManager.LoadData();
            //shellManager.LoadData();
            trackManager.LoadData();
            turretManager.LoadData();
        }

        public void SetArmorById(string id)
        {
            CurrentArmorData = armorManager.GetArmorById(id);
        }

        public void SetEngineById(string id)
        {
            CurrentEngineData = engineManager.GetEngineById(id);
        }

        /* TODO: Uncomment when shell data is available
        public void SetShellById(string id)
        {
            CurrentShellData = shellManager.GetShellById(id);
        }
        */

        public void SetTrackById(string id)
        {
            CurrentTrackData = trackManager.GetTrackById(id);
        }

        public void SetTurretById(string id)
        {
            CurrentTurretData = turretManager.GetTurretById(id);
        }
    
    }
}
