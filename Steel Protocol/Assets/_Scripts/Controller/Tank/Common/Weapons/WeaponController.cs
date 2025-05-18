using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Tank.Common.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        // Input interface for firing weapons
        private IInputSource input;

        // Controllers for the different weapon slots
        [Header("Weapon Slot Controllers")]
        [SerializeField] private MainWeaponController mainWeapon;
        [SerializeField] private SecWeaponController secondaryWeapon;
        [SerializeField] private TerWeaponController tertiaryWeapon;


        private void Awake()
        {
            input = GetComponent<IInputSource>();
        }


        // I really don't like this but this is the best I can do for now
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


        // Null propagation in Unity is a bitch
        // This looks awful but it works
        // Credits to "iPlayTehGames" for the code snippet example: https://www.reddit.com/r/Unity3D/comments/1109104/null_propagation_operator_and_unity/
        
        // Fires the main weapon if it exists
        public void FireMain()
        {
            if (mainWeapon != null)
            {
                mainWeapon.TryFire();
            }
        }


        // Fires the secondary weapon if it exists
        public void FireSecondary()
        {
            if (secondaryWeapon != null)
            {
                secondaryWeapon.TryFire();
            }        
        }


        // Fires the tertiary weapon if it exists
        public void FireTertiary()
        {
            if (tertiaryWeapon != null)
            {
                tertiaryWeapon.TryFire();
            }
        }
    }
}