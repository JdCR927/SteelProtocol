using UnityEngine;
using System.IO;

namespace SteelProtocol.Data.Save
{
    public static class LoadoutSaveSystem
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "playerLoadout.json");

        public static void SaveLoadout(LoadoutSaveData loadout)
        {
            string json = JsonUtility.ToJson(loadout, true);
            File.WriteAllText(SavePath, json);
        }

        public static LoadoutSaveData LoadLoadout()
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                return JsonUtility.FromJson<LoadoutSaveData>(json);
            }
            return null; // or return a new LoadoutSaveData() if you want to ensure a valid object
        }
    }
}
