using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Player
{
    [RequireComponent(typeof(TurretController))]
    [RequireComponent(typeof(IInputSource))]
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
            // Get the input values for aiming
            aiming.Aim(input.GetLookInput());

            //////////////////////////////////////////////////////////////
            // ToDo: Debug the input values for mouse sensitivity stuff //
            //////////////////////////////////////////////////////////////
            Debug.Log("Aiming: " + input.GetLookInput());
        }

    }
}