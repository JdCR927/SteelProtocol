using UnityEngine;
using System.IO;

namespace SteelProtocol.Data.Save
{
    public static class AudioSaveSystem
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "audioSettings.json");

        public static void SaveAudioSettings(AudioSaveData settings)
        {
            string json = JsonUtility.ToJson(settings, true);
            File.WriteAllText(SavePath, json);
        }

        public static AudioSaveData LoadAudioSettings()
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                return JsonUtility.FromJson<AudioSaveData>(json);
            }
            return new AudioSaveData
            {
                musicVolume = 0.5f,
                sfxVolume = 0.5f
            };
        }
    }
}