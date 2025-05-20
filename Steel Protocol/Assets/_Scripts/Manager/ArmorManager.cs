using UnityEngine;
using SteelProtocol.Data.Armor;

namespace SteelProtocol.Manager
{
    public class ArmorManager : MonoBehaviour
    {
        private ArmorData[] armors;

        public void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("JSON/armors");
            if (jsonFile == null)
            {
                Debug.LogError("armor.json not found in Resources/JSON/");
                return;
            }

            string wrappedJson = "{\"armors\":" + jsonFile.text + "}";
            ArmorDataList dataList = JsonUtility.FromJson<ArmorDataList>(wrappedJson);
            armors = dataList.armors;
        }

        public ArmorData GetArmorById(string id)
        {
            foreach (ArmorData armor in armors)
            {
                if (armor.id == id)
                {
                    return armor;
                }
            }

            Debug.LogError("Armor with ID " + id + " not found.");
            return null;
        }
    }
}