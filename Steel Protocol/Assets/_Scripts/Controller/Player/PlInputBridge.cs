using UnityEngine;
using UnityEngine.InputSystem;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Player
{
    /// <summary>
    /// Handles player movement input via UnityEvents and exposes it to the tank controller.
    /// Implements IInputSource to be used in modular systems.
    /// </summary>
    public class PlayerInputRelay : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;
        private Vector2 lookInput;

        /// <summary>
        /// Callback from Unity's input system for movement.
        /// Stores 2D movement vector (x = turn, y = forward).
        /// </summary>
        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
                lookInput = context.ReadValue<Vector2>();
            else if (context.canceled)
                lookInput = Vector2.zero;
        }

        // Lambda Methods
        public float GetForwardInput() => movementInput.y;
        public float GetTurnInput() => movementInput.x;
        public Vector2 GetLookInput() => lookInput;
    }
}
