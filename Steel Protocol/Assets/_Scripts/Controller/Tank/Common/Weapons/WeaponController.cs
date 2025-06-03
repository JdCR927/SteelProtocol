using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Tank.Common.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        // Input interface for firing weapons
        private IInputSource input;

        // Controllers for the different weapon slots
        [Header("Weapon controllers")]
        [SerializeField] private MainWeaponController mainWeapon;


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

    }
}