using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Scenes;

namespace SteelProtocol.UI.MainMenu
{
    public class MainMenuEvents : MonoBehaviour
    {

        private UIDocument document;

        private VisualElement mainMenu, settingsMenu;
        private Button startButton, settingsButton, returnButton, exitButton;
        private SliderInt sfxSlider, musicSlider;

        private void Awake()
        {
            // Initialize the UI Document and get references to the UI elements
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
            returnButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
        }


        private void OnStartButtonClicked(ClickEvent evt)
        {
            // Load overworld scene
            StartCoroutine(SceneChanger.LoadSceneNoTransition(EnumScenes.Overworld.ToString()));
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            if (settingsMenu.style.display == DisplayStyle.None)
            {
                // Show settings menu
                settingsMenu.style.display = DisplayStyle.Flex;
                mainMenu.style.display = DisplayStyle.None;
            }
            else
            {
                // Hide settings menu
                settingsMenu.style.display = DisplayStyle.None;
                mainMenu.style.display = DisplayStyle.Flex;
            }
        }

        public void OnReturnButtonClicked(ClickEvent evt)
        {
            if (mainMenu.style.display == DisplayStyle.None)
            {
                // Hide settings menu
                settingsMenu.style.display = DisplayStyle.None;
                mainMenu.style.display = DisplayStyle.Flex;
            }
            else
            {
                // Show settings menu
                settingsMenu.style.display = DisplayStyle.Flex;
                mainMenu.style.display = DisplayStyle.None;
            }
        }

        private static void OnExitButtonClicked(ClickEvent evt)
        {
            Application.Quit();
        }
    }
}