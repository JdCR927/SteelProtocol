using UnityEngine;

namespace SteelProtocol.Input
{
    public interface IInputSource
    {
        float GetForwardInput();
        float GetTurnInput();

        Vector2 GetLookInput();

        bool IsFiringMain();
    }
}
