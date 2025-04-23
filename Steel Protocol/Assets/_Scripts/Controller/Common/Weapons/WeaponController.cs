using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Common.Weapons
{
    [RequireComponent(typeof(IInputSource))]
    public class WeaponController : MonoBehaviour
    {
        private IInputSource input;

        [Header("Weapon Slot Controllers")]
        [SerializeField] private MainWeaponController mainWeapon;
        [SerializeField] private SecWeaponController secondaryWeapon;
        [SerializeField] private TerWeaponController tertiaryWeapon;


        private void Awake()
        {
            input = GetComponent<IInputSource>();
        }

        private void Update()
        {
            if (input == null) return;

            if (input.IsFiringMain())
                FireMain();

            if (input.IsFiringSec())
                FireSecondary();

            if (input.IsFiringTer())
                FireTertiary();
        }

        public void FireMain()
        {
            mainWeapon?.TryFire();
        }

        public void FireSecondary()
        {
            secondaryWeapon?.TryFire();
        }

        public void FireTertiary()
        {
            tertiaryWeapon?.TryFire();
        }
    }
}