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

            // Initialize the dropdowns with the corresponding data
            armorDrop.Init(EnumArmor.normalArmor);
            engineDrop.Init(EnumEngine.normalEngine);
            shellDrop.Init(EnumShell.armorPiercing);
            trackDrop.Init(EnumTrack.normalTracks);
            turretDrop.Init(EnumTurret.standardDrive);

            armorId = ((EnumArmor)armorDrop.value).ToString();
            engineId = ((EnumEngine)engineDrop.value).ToString();
            shellId = ((EnumShell)shellDrop.value).ToString();
            trackId = ((EnumTrack)trackDrop.value).ToString();
            turretId = ((EnumTurret)turretDrop.value).ToString();

            // Initialize the button
            btnExit = document.rootVisualElement.Q<Button>("BtnExit");

            // Register callbacks for button clicks
            btnExit.RegisterCallback<ClickEvent>(OnBtnExitClick);
        }


        private void OnEnable()
        {
            ArmorRegisterChange();
            EngineRegisterChange();
            ShellRegisterChange();
            TrackRegisterChange();
            TurretRegisterChange();
        }

        private void ArmorRegisterChange()
        {
            armorDrop.RegisterValueChangedCallback(evt =>
            {
                armorId = ((EnumArmor)evt.newValue).ToString();
            });
        }

        private void EngineRegisterChange()
        {
            engineDrop.RegisterValueChangedCallback(evt =>
            {
                engineId = ((EnumEngine)evt.newValue).ToString();
            });
        }

        private void ShellRegisterChange()
        {
            shellDrop.RegisterValueChangedCallback(evt =>
            {
                shellId = ((EnumShell)evt.newValue).ToString();
            });
        }

        private void TrackRegisterChange()
        {
            trackDrop.RegisterValueChangedCallback(evt =>
            {
                trackId = ((EnumTrack)evt.newValue).ToString();
            });
        }

        private void TurretRegisterChange()
        {
            turretDrop.RegisterValueChangedCallback(evt =>
            {
                turretId = ((EnumTurret)evt.newValue).ToString();
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