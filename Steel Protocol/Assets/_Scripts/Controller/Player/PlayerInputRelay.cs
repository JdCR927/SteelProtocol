using UnityEngine;
using UnityEngine.InputSystem;

namespace SteelProtocol.Input
{
    public class PlayerInputRelay : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public float GetForwardInput() => movementInput.y;
        public float GetTurnInput() => movementInput.x;
    }
}
