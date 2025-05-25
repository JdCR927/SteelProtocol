using UnityEngine;
using UnityEngine.UIElements;
using SteelProtocol.Controller.Tank.Common.Weapons;

namespace SteelProtocol.UI.HUD
{
    public class AmmobarController : MonoBehaviour
    {
        [SerializeField] private MainWeaponController weapon;

        private void OnEnable()
        {
            VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
            var ammoBar = root.Q<AmmoBar>("MainAmmo");

            weapon.OnAmmoChanged += (current, max) => ammoBar.SetAmmo(current, max);
            weapon.OnReloadStarted += time => ammoBar.StartReload(time);
        }
    }
}