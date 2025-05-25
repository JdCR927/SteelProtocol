using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace SteelProtocol.UI.HUD
{
    public class AiHealthbarController : MonoBehaviour
    {
        [SerializeField] private float hideDelay = 3f;

        private HealthBar enemyHealthBar;
        private VisualElement root;
        private Coroutine hideCoroutine;

        void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            enemyHealthBar = root.Q<HealthBar>("EnemyHealthbar");
            enemyHealthBar.style.display = DisplayStyle.None;
        }

        public void ShowEnemyHealth(float current, float max)
        {
            float percent = current / max * 100f;
            enemyHealthBar.Progress = percent;
            enemyHealthBar.style.display = DisplayStyle.Flex;

            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(hideDelay);
            enemyHealthBar.style.display = DisplayStyle.None;
        }
    }
}
