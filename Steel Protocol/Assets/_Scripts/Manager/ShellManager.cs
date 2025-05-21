using UnityEngine;
using SteelProtocol.Data.Shell;

namespace SteelProtocol.Manager
{ 
    public class ShellManager : MonoBehaviour
    {
        private ShellData[] shells;

        public void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("JSON/shells");
            if (jsonFile == null)
            {
                Debug.LogError("shells.json not found in Resources/JSON/");
                return;
            }

            string wrappedJson = "{\"shells\":" + jsonFile.text + "}";
            ShellDataList dataList = JsonUtility.FromJson<ShellDataList>(wrappedJson);
            shells = dataList.shells;
        }

        public ShellData GetShellById(string id)
        {
            foreach (var shell in shells)
            {
                if (shell.id == id)
                    return shell;
            }

            Debug.LogWarning($"Shell ID '{id}' not found.");
            return null;
        }
    }
}