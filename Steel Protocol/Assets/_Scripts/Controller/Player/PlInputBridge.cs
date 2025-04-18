using UnityEngine;
using UnityEngine.InputSystem;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Player
{
    /// <summary>
    /// Handles player movement input via UnityEvents and exposes it to the tank controller.
    /// Implements IInputSource to be used in modular systems.
    /// </summary>
    public class PlInputBridge : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;
        private bool fireSecondary;
        private bool fireTertiary;

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

        public void OnAttack1(InputAction.CallbackContext context)
        {
            fireMain = context.ReadValue<float>() > 0.5f;
        }

        public void OnAttack2(InputAction.CallbackContext context)
        {
            fireSecondary = context.ReadValue<float>() > 0.5f;
        }

        public void OnAttack3(InputAction.CallbackContext context)
        {
            fireTertiary = context.ReadValue<float>() > 0.5f;
        }

        // Lambda Methods
        public float GetForwardInput() => movementInput.y;
        public float GetTurnInput() => movementInput.x;
        public Vector2 GetLookInput() => lookInput;
        public bool IsFiringMain() => fireMain;
        public bool IsFiringSec() => fireSecondary;
        public bool IsFiringTer() => fireTertiary;
    }
}
