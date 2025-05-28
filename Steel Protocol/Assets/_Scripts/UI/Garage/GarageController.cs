using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Scenes;
using SteelProtocol.Data.Enum;
using SteelProtocol.Data.Save;

namespace SteelProtocol.UI.Garage
{
    public partial class GarageController : MonoBehaviour
    {
        private UIDocument document;
        private EnumField armorDrop, engineDrop, shellDrop, trackDrop, turretDrop;
        private Button btnExit;


        private string armorId, engineId, shellId, trackId, turretId;

        private void Awake()
        {
            // Initialize the UI Document
            document = GetComponent<UIDocument>();

            // Initialize the dropdowns
            armorDrop = document.rootVisualElement.Q<EnumField>("ArmorDrop");
            engineDrop = document.rootVisualElement.Q<EnumField>("EngineDrop");
            shellDrop = document.rootVisualElement.Q<EnumField>("ShellDrop");
            trackDrop = document.rootVisualElement.Q<EnumField>("TrackDrop");
            turretDrop = document.rootVisualElement.Q<EnumField>("TurretDrop");

            LoadCurrentLoadout();

            ValuesToString();

            // Initialize the button
            btnExit = document.rootVisualElement.Q<Button>("BtnExit");

            // Register callbacks for button clicks
            btnExit.RegisterCallback<ClickEvent>(OnBtnExitClick);
        }

        private void LoadCurrentLoadout()
        {
            LoadoutSaveData data = LoadoutSaveSystem.LoadLoadout();
            if (data != null)
            {
                if (System.Enum.TryParse(data.armorId, out EnumArmor armor))
                    armorDrop.value = armor;
                if (System.Enum.TryParse(data.engineId, out EnumEngine engine))
                    engineDrop.value = engine;
                if (System.Enum.TryParse(data.shellId, out EnumShell shell))
                    shellDrop.value = shell;
                if (System.Enum.TryParse(data.trackId, out EnumTrack track))
                    trackDrop.value = track;
                if (System.Enum.TryParse(data.turretId, out EnumTurret turret))
                    turretDrop.value = turret;
            }
        }

        private void ValuesToString()
        {
            armorId = ((EnumArmor)armorDrop.value).ToString();
            engineId = ((EnumEngine)engineDrop.value).ToString();
            shellId = ((EnumShell)shellDrop.value).ToString();
            trackId = ((EnumTrack)trackDrop.value).ToString();
            turretId = ((EnumTurret)turretDrop.value).ToString();
        }

        private void OnEnable()
        {
            RegisterChange<EnumArmor>(armorDrop, val => armorId = val);
            RegisterChange<EnumEngine>(engineDrop, val => engineId = val);
            RegisterChange<EnumShell>(shellDrop, val => shellId = val);
            RegisterChange<EnumTrack>(trackDrop, val => trackId = val);
            RegisterChange<EnumTurret>(turretDrop, val => turretId = val);
        }

        private static void RegisterChange<T>(EnumField field, System.Action<string> setter) where T : System.Enum
        {
            field.RegisterValueChangedCallback(evt =>
            {
                setter(((T)evt.newValue).ToString());
            });
        }

        private LoadoutSaveData CreateLoadout()
        {
            var data = new LoadoutSaveData
            {
                armorId = armorId.ToString(),
                engineId = engineId.ToString(),
                shellId = shellId.ToString(),
                trackId = trackId.ToString(),
                turretId = turretId.ToString()
            };

            return data;
        }

        private void OnBtnExitClick(ClickEvent evt)
        {
            LoadoutSaveSystem.SaveLoadout(CreateLoadout());

            StartCoroutine(SceneChanger.LoadSceneNoTransition(EnumScenes.Overworld.ToString()));
        }
    }
}