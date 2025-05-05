using UnityEngine;
using SteelProtocol.Input;

namespace SteelProtocol.Controller.AI
{
    public class AiInputBridge : MonoBehaviour, IInputSource
    {
        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;
        private bool fireSecondary;
        private bool fireTertiary;


        /* TODO:
        public void OnMove( context)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        */


        public void OnLook(/*SomethingWhatever target*/)
        {
            
        }


        /* TODO:
        public void OnAttack1( context)
        {
            fireMain = context.ReadValue<float>() > 0.5f;
        }
        */

        /* TODO:
        public void OnAttack2( context)
        {
            fireSecondary = context.ReadValue<float>() > 0.5f;
        }
        */


        /* TODO:
        public void OnAttack3( context)
        {
            fireTertiary = context.ReadValue<float>() > 0.5f;
        }
        */


        public float GetForwardInput() => movementInput.y;


        public float GetTurnInput() => movementInput.x;


        public Vector2 GetLookInput() => lookInput;


        public bool IsFiringMain() => fireMain;


        public bool IsFiringSec() => fireSecondary;
        
        
        public bool IsFiringTer() => fireTertiary;
    }
}
