using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Player
{
    [RequireComponent(typeof(TurretController))]
    public class PlTurretController: MonoBehaviour
    {
        // Interface for input handling
        private IInputSource input;

        // Controller for aiming
        private TurretController aiming;


        private void Awake()
        {
            // Get the input source and aiming controller components
            input = GetComponent<IInputSource>();
            aiming = GetComponent<TurretController>();

            // Check if the components are assigned correctly
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
            ///////////////////////////////////////////////////////////////////////////
            // ToDo: Cap the value at 1 maybe? 1 is the max value for gamepads so... //
            ///////////////////////////////////////////////////////////////////////////
            // Get the input values for aiming
            aiming.Aim(input.GetLookInput());
        }

    }
}