using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Player
{
    /// <summary>
    /// Handles turret yaw (horizontal) and gun elevation (vertical) for the player's tank.
    /// Reacts to Look input events and smooths turret/gun movement.
    /// </summary>
    [RequireComponent(typeof(TurretController))]
    [RequireComponent(typeof(IInputSource))]
    public class PlTurretController: MonoBehaviour
    {
        private IInputSource input;
        private TurretController aiming;


        private void Awake()
        {
            input = GetComponent<IInputSource>();
            aiming = GetComponent<TurretController>();

            if (input == null)
            {
                Debug.LogError("Missing input component");
            }
            else if (aiming == null)
            {
                Debug.LogError("Missing aiming component");
            }
        }

        private void FixedUpdate()
        {
            aiming.Aim(input.GetLookInput());
        }

    }
}