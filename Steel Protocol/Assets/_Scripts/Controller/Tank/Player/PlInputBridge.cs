using UnityEngine;
using UnityEngine.InputSystem;
using SteelProtocol.Input;
using SteelProtocol.UI.PauseMenu;

namespace SteelProtocol.Controller.Tank.Player
{
    [DisallowMultipleComponent]
    public class PlInputBridge : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;


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
            if (Time.timeScale == 0f) return; // Lazy, but fuck you

            fireMain = context.performed;
        }


        public static void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                TogglePause();
            }
        }

        private static void TogglePause()
        {
            bool isPaused = Time.timeScale == 0f;
            Time.timeScale = isPaused ? 1f : 0f;

            var pauseMenu = FindFirstObjectByType<PauseMenuEvents>();
            if (pauseMenu != null)
            {
                pauseMenu.SetPauseMenuVisible(!isPaused);
            }
            else
            {
                Debug.Log("Pause menu not found.");
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
    }
}
