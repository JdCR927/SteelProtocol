using SteelProtocol.Data.Enum;
using SteelProtocol.Data.Save;

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

            if (System.Enum.TryParse(data.armorId, out EnumArmor armor))
                armorId = armor;
            if (System.Enum.TryParse(data.engineId, out EnumEngine engine))
                engineId = engine;
            if (System.Enum.TryParse(data.shellId, out EnumShell shell))
                shellId = shell;
            if (System.Enum.TryParse(data.trackId, out EnumTrack track))
                trackId = track;
            if (System.Enum.TryParse(data.turretId, out EnumTurret turret))
                turretId = turret;
        }
    }
}
