using SteelProtocol.Input;
using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Controller.Tank.Player
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(PlInputBridge))]
    public class PlMovementController : MonoBehaviour
    {
        private IInputSource input;
        private MovementController movement;


        private void Awake()
        {
            // Get the input source and movement controller components
            input = GetComponent<IInputSource>();
            movement = GetComponent<MovementController>();
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
