using UnityEngine;
using SteelProtocol.Data.Turret;

namespace SteelProtocol.Manager
{
    public class TurretManager : MonoBehaviour
    {
        private TurretData[] turrets;


        public void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("JSON/turrets");
            if (jsonFile == null)
            {
                Debug.LogError("turrets.json not found in Resources/JSON/");
                return;
            }

            string wrappedJson = "{\"turrets\":" + jsonFile.text + "}";
            TurretDataList dataList = JsonUtility.FromJson<TurretDataList>(wrappedJson);
            turrets = dataList.turrets;
        }

        public TurretData GetTurretById(string id)
        {
            foreach (var turret in turrets)
            {
                if (turret.id == id)
                    return turret;
            }

            Debug.LogWarning($"Turret ID '{id}' not found.");
            return null;
        }
    }
}
