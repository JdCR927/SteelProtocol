using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Scenes;

namespace SteelProtocol.UI.MainMenu
{
    public class MainMenuEvents : MonoBehaviour
    {

        private UIDocument document;

        private Button startButton;
        private Button settingsButton;
        private Button exitButton;


        private void Awake()
        {
            document = GetComponent<UIDocument>();

            startButton = document.rootVisualElement.Q<Button>("BtnStart");
            settingsButton = document.rootVisualElement.Q<Button>("BtnSettings");
            exitButton = document.rootVisualElement.Q<Button>("BtnExit");

            startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
            settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);

        }


        private void OnStartButtonClicked(ClickEvent evt)
        {
            // Load overworld scene
            StartCoroutine(SceneChanger.LoadSceneNoTransition(EnumScenes.Overworld.ToString()));
        }

        private static void OnSettingsButtonClicked(ClickEvent evt)
        {
            Debug.Log("Settings button clicked!");
        }

        private static void OnExitButtonClicked(ClickEvent evt)
        {
            Application.Quit();
        }
    }
}