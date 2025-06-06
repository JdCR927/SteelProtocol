using System.Collections;
using SteelProtocol.Scenes;
using UnityEngine;
using UnityEngine.UIElements;

namespace SteelProtocol.UI.MissionEnd
{
    public class MissionEndEvents : MonoBehaviour
    {
        [SerializeField] private MissionManager manager;

        private UIDocument document;
        private VisualElement panel, clearMenu, failMenu;

        private void Awake()
        {
            // Initialize the UI Document
            document = GetComponent<UIDocument>();

            // Initialize the visual elements
            panel = document.rootVisualElement.Q<VisualElement>("Panel");
            clearMenu = document.rootVisualElement.Q<VisualElement>("ClearMenu");
            failMenu = document.rootVisualElement.Q<VisualElement>("FailedMenu");
        }


        private void OnEnable()
        {
            if (manager != null)
            {
                manager.OnVictory += OnClearCondition;
                manager.OnDefeat += OnFailContinue;
            }
        }

        private void OnDisable()
        {
            if (manager != null)
            {
                manager.OnVictory -= OnClearCondition;
                manager.OnDefeat -= OnFailContinue;
            }
        }

        private void OnClearCondition()
        {
            EnablePanel();
            clearMenu.style.display = DisplayStyle.Flex;

            StartCoroutine(WaitAndChange(3f));
        }

        private void OnFailContinue()
        {
            EnablePanel();
            failMenu.style.display = DisplayStyle.Flex;

            StartCoroutine(WaitAndChange(3f));
        }

        private void EnablePanel()
        {
            panel.style.display = DisplayStyle.Flex;
        }

        private void DisableAll()
        {
            panel.style.display = DisplayStyle.None;
            clearMenu.style.display = DisplayStyle.None;
            failMenu.style.display = DisplayStyle.None;
        }

        private IEnumerator WaitAndChange(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            DisableAll();
            StartCoroutine(SceneChanger.LoadScene(EnumScenes.Overworld.ToString()));
        }
    }
}
