using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Scenes;

namespace SteelProtocol.UI.MainMenu
{
    // Many thanks to Sasquatch B Studios for providing a great tutorial on how to use UI Toolkit
    // Get started with UI Toolkit in Unity - https://www.youtube.com/watch?v=_jtj73lu2Ko
    public class MainMenuEvents : MonoBehaviour
    {

        private UIDocument document;

        private VisualElement mainMenu, settingsMenu;
        private Button startButton, settingsButton, returnButton, exitButton;
        private SliderInt sfxSlider, musicSlider;

        private void Awake()
        {
            // Initialize the UI Document
            document = GetComponent<UIDocument>();

            // Initialize the visual elements
            mainMenu = document.rootVisualElement.Q<VisualElement>("MainMenuContainer");
            settingsMenu = document.rootVisualElement.Q<VisualElement>("SettingsContainer");

            // Initialize the buttons
            startButton = document.rootVisualElement.Q<Button>("BtnStart");
            settingsButton = document.rootVisualElement.Q<Button>("BtnSettings");
            returnButton = document.rootVisualElement.Q<Button>("BtnReturn");
            exitButton = document.rootVisualElement.Q<Button>("BtnExit");

            // Initialize the sliders
            sfxSlider = document.rootVisualElement.Q<SliderInt>("SfxSlider");
            musicSlider = document.rootVisualElement.Q<SliderInt>("MusicSlider");

            // Set the initial state of the visual elements
            mainMenu.style.display = DisplayStyle.Flex;
            settingsMenu.style.display = DisplayStyle.None;

            // Register callbacks for button clicks
            startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
            settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            returnButton.RegisterCallback<ClickEvent>(OnReturnButtonClicked);
            exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
        }


        private void OnStartButtonClicked(ClickEvent evt)
        {
            // Load overworld scene
            StartCoroutine(SceneChanger.LoadSceneNoTransition(EnumScenes.Overworld.ToString()));
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            bool switchingToSettings = mainMenu.style.display != DisplayStyle.None;

            mainMenu.style.display = switchingToSettings ? DisplayStyle.None : DisplayStyle.Flex;
            settingsMenu.style.display = switchingToSettings ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void OnReturnButtonClicked(ClickEvent evt)
        {
            bool switchingToMain = settingsMenu.style.display != DisplayStyle.None;

            mainMenu.style.display = switchingToMain ? DisplayStyle.Flex : DisplayStyle.None;
            settingsMenu.style.display = switchingToMain ? DisplayStyle.None : DisplayStyle.Flex;
        }

        private static void OnExitButtonClicked(ClickEvent evt)
        {
            Application.Quit();
        }
    }
}