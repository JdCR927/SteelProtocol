using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Controller.Player
{
    /// <summary>
    /// Controls the player's tank by combining input and movement logic.
    /// Inherits from TankController and delegates actual movement to MovementController.
    /// </summary>
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(IInputSource))]
    public class PlTankController : TankController
    {
        private IInputSource input;
        private MovementController movement;

        private void Awake()
        {
            input = GetComponent<IInputSource>();
            movement = GetComponent<MovementController>();

            if (input == null)
                Debug.LogError("IInputSource not found.");
            if (movement == null)
                Debug.LogError("TankMovementController not found.");
        }

        private void FixedUpdate()
        {
            movement.Move(input.GetForwardInput(), input.GetTurnInput());
        }

    }
}
