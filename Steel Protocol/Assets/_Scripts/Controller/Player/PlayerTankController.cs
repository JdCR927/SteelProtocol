using SteelProtocol.Input;
using UnityEngine;

namespace SteelProtocol.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IInputSource))]
    public class PlayerTankController : TankController
    {
        private IInputSource input;

        private void Awake()
        {
            input = GetComponent<IInputSource>();

            if (input == null)
            {
                Debug.LogError("InputProvider does not implement IInputSource.");
            }
        }

        protected override float GetForwardInput() => input?.GetForwardInput() ?? 0f;

        protected override float GetTurnInput() => input?.GetTurnInput() ?? 0f;
    }
}
