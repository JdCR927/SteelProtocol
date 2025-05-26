using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Scenes;

namespace SteelProtocol.UI.PauseMenu
{
    public class PauseMenuEvents : MonoBehaviour
    {

        private UIDocument document;
        private VisualElement pausePanel, pauseMenu, settingsMenu;
        private Button continueButton, settingsButton, returnButton, restartButton, levelButton, menuButton;
        private SliderInt SFXSlider, MusicSlider;

        private void Awake()
        {
            // Initialize the UI Document
            document = GetComponent<UIDocument>();

            // Initialize the visual elements
            pausePanel = document.rootVisualElement.Q<VisualElement>("Panel");
            pauseMenu = document.rootVisualElement.Q<VisualElement>("PauseMenu");
            settingsMenu = document.rootVisualElement.Q<VisualElement>("SettingsMenu");

            // Initialize the buttons
            continueButton = document.rootVisualElement.Q<Button>("BtnContinue");
            settingsButton = document.rootVisualElement.Q<Button>("BtnSettings");
            returnButton = document.rootVisualElement.Q<Button>("BtnReturn");
            restartButton = document.rootVisualElement.Q<Button>("BtnRestart");
            levelButton = document.rootVisualElement.Q<Button>("BtnExitLevel");
            menuButton = document.rootVisualElement.Q<Button>("BtnMenu");

            // Initialize the sliders
            SFXSlider = document.rootVisualElement.Q<SliderInt>("SFXSlider");
            MusicSlider = document.rootVisualElement.Q<SliderInt>("MusicSlider");

            // Set the initial state of the entire pause menu system
            pausePanel.style.display = DisplayStyle.None;
            pauseMenu.style.display = DisplayStyle.Flex;
            settingsMenu.style.display = DisplayStyle.None;

            // Register callbacks for button clicks
            continueButton.RegisterCallback<ClickEvent>(OnContinueButtonClicked);
            settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            returnButton.RegisterCallback<ClickEvent>(OnReturnButtonClicked);
            restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);
            levelButton.RegisterCallback<ClickEvent>(OnLevelButtonClicked);
            menuButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
        }


        private void OnContinueButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1f; // Resume the game
            SetPauseMenuVisible(false);
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            bool switchingToSettings = pauseMenu.style.display != DisplayStyle.None;

            pauseMenu.style.display = switchingToSettings ? DisplayStyle.None : DisplayStyle.Flex;
            settingsMenu.style.display = switchingToSettings ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void OnReturnButtonClicked(ClickEvent evt)
        {
            bool switchingToPause = settingsMenu.style.display != DisplayStyle.None;

            pauseMenu.style.display = switchingToPause ? DisplayStyle.Flex : DisplayStyle.None;
            settingsMenu.style.display = switchingToPause ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public void OnRestartButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1f; // Resume the game first
            SetPauseMenuVisible(false); // Hide the pause menu
            StartCoroutine(SceneChanger.ReloadCurrentScene()); // Reloads the current scene
        }

        private void OnLevelButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1f;
            SetPauseMenuVisible(false);
            StartCoroutine(SceneChanger.LoadScene(EnumScenes.Overworld.ToString()));
        }

        private void OnExitButtonClicked(ClickEvent evt)
        {
            Time.timeScale = 1f; // Resume the game first
            SetPauseMenuVisible(false); // Hide the pause menu
            StartCoroutine(SceneChanger.LoadScene(EnumScenes.MainMenu.ToString())); // Load the main menu scene
        }


        public void SetPauseMenuVisible(bool isPaused)
        {
            pausePanel.style.display = isPaused ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
