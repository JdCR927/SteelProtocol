using UnityEngine;
using UnityEngine.InputSystem;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.Player
{
    public class PlInputBridge : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;
        private bool fireSecondary;
        private bool fireTertiary;
        private bool exitGame;


        // Get's the context from the input system and assigns it to the movementInput variable
        // This is called when the player presses the move button (WASD)
        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }


        // Get's the context from the input system and assigns it to the lookInput variable
        // This is called when the player moves the mouse or right joystick
        // When the player releases the mouse or joystick, it sets the input to zero
        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
                lookInput = context.ReadValue<Vector2>();
            else if (context.canceled)
                lookInput = Vector2.zero;
        }


        // Get the context from when the player presses the fire button (Left mouse button or right trigger)
        public void OnAttack1(InputAction.CallbackContext context)
        {
            fireMain = context.ReadValue<float>() > 0.5f;
        }


        // Get the context from when the player presses the fire button (Right mouse button or left trigger)
        public void OnAttack2(InputAction.CallbackContext context)
        {
            fireSecondary = context.ReadValue<float>() > 0.5f;
        }


        // Get the context from when the player presses the fire button (Middle mouse button or left shoulder)
        public void OnAttack3(InputAction.CallbackContext context)
        {
            fireTertiary = context.ReadValue<float>() > 0.5f;
        }


        public void OnExit(InputAction.CallbackContext context)
        {
            exitGame = context.ReadValue<float>() > 0.5f;

            if (exitGame)
            {
                Debug.Log("Exiting game...");
                Application.Quit();
            }
        }


        // Get's the movement input from the player, specifically the W and S keys
        // Or the vertical movement of the left joystick for forward and backward movement
        public float GetForwardInput() => movementInput.y;


        // Get's the movement input from the player, specifically the A and D keys 
        // Or the horizontal movement of the left joystick for left and right movement
        public float GetTurnInput() => movementInput.x;


        // Get's the mouse (Or right joystick) input from the player
        public Vector2 GetLookInput() => lookInput;


        // Get's the fire input from the player, specifically the left mouse button (Or right trigger)
        public bool IsFiringMain() => fireMain;


        // Get's the fire input from the player, specifically the right mouse button (Or left trigger)
        public bool IsFiringSec() => fireSecondary;
        

        // Get's the fire input from the player, specifically the middle mouse button (Or left shoulder)
        public bool IsFiringTer() => fireTertiary;

        public bool IsExiting() => exitGame;
    }
}
