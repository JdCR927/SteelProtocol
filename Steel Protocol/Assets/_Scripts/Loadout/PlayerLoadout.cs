using SteelProtocol.Data.Save;
using UnityEngine;

namespace SteelProtocol.Loadout
{
    public class PlayerLoadout : Loadout
    {
        private void Awake()
        {
            LoadLoadout();
        }

        private void SaveLoadout()
        {
            var data = new LoadoutSaveData
            {
                armorId = armorId.ToString(),
                engineId = engineId.ToString(),
                shellId = shellId.ToString(),
                trackId = trackId.ToString(),
                turretId = turretId.ToString()
            };

            LoadoutSaveSystem.SaveLoadout(data);
        }

        private void LoadLoadout()
        {
            var data = LoadoutSaveSystem.LoadLoadout();
            if (data == null)
                SaveLoadout(); // If no data exists, save default loadout

            if (System.Enum.TryParse(data.armorId, out ArmorType armor))
                armorId = armor;
            if (System.Enum.TryParse(data.engineId, out EngineType engine))
                engineId = engine;
            if (System.Enum.TryParse(data.shellId, out ShellType shell))
                shellId = shell;
            if (System.Enum.TryParse(data.trackId, out TrackType track))
                trackId = track;
            if (System.Enum.TryParse(data.turretId, out TurretType turret))
                turretId = turret;
        }
    }
}
