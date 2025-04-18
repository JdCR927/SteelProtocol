using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Common
{
    /// <summary>
    /// Generic weapon system controller that delegates firing to multiple weapon slots.
    /// </summary>
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

        // ToDo: Absolute hack, get rid of this ASAP.
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