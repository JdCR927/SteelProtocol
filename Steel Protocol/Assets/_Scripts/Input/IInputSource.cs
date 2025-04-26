using UnityEngine;

namespace SteelProtocol.Input
{
    public interface IInputSource
    {
        #region Movement
        float GetForwardInput();
        float GetTurnInput();
        #endregion

        #region Aiming
        Vector2 GetLookInput();
        #endregion

        #region Weapons
        bool IsFiringMain();
        bool IsFiringSec();
        bool IsFiringTer();
        #endregion

        bool IsExiting();
    }
}
