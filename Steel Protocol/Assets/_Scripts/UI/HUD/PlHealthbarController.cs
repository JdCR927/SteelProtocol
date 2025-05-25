using SteelProtocol.Controller.Tank.Common.HP;
using UnityEngine;
using UnityEngine.UIElements;

namespace SteelProtocol.UI.HUD
{
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
