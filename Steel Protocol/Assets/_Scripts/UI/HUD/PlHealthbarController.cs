using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Controller.Tank.Common.HP;

namespace SteelProtocol.UI.HUD
{
    // Thanks to Game Dev Guide for the helping hand on this script
    // Building Runtime UI with UI Toolkit In Unity - https://www.youtube.com/watch?v=6DcwHPxCE54
    public class PlHealthbarController : MonoBehaviour
    {
        public HealthController tank;

        void OnEnable()
        {
            VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
            var healthBar = root.Q<HealthBar>("Healthbar");
            tank.OnHealthChanged += percent => healthBar.Progress = percent;
        }
    }
}
