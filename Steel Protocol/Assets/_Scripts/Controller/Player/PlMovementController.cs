using SteelProtocol.Manager;
using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Player
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(IInputSource))]
    public class PlMovementController : MonoBehaviour
    {
        // Interface for input handling
        private IInputSource input;

        // Controller for movement
        private MovementController movement;


        private void Awake()
        {
            // Get the input source and movement controller components
            input = GetComponent<IInputSource>();
            movement = GetComponent<MovementController>();

            // Check if the components are assigned correctly
            if (input == null)
                Debug.LogError("IInputSource not found.");
            if (movement == null)
                Debug.LogError("TankMovementController not found.");
        }


        private void FixedUpdate()
        {
            // Get the input values for forward movement and turning
            float forwardInput = input.GetForwardInput();
            float turnInput = input.GetTurnInput();

            // Move the tank based on the input values
            movement.Move(forwardInput, turnInput);

        }

    }
}
