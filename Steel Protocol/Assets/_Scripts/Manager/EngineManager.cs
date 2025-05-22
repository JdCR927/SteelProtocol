using UnityEngine;
using SteelProtocol.Data.Engine;

namespace SteelProtocol.Manager
{
    public class EngineManager : MonoBehaviour
    {
        private EngineData[] engines;

        public void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("JSON/engines");
            if (jsonFile == null)
            {
                Debug.LogError("engines.json not found in Resources/JSON/");
                return;
            }

            string wrappedJson = "{\"engines\":" + jsonFile.text + "}";
            EngineDataList dataList = JsonUtility.FromJson<EngineDataList>(wrappedJson);
            engines = dataList.engines;
        }

        public EngineData GetEngineById(string id)
        {
            foreach (EngineData engine in engines)
            {
                if (engine.id == id)
                {
                    return engine;
                }
            }

            Debug.LogError("Engine with ID " + id + " not found.");
            return null;
        }
    }
}
