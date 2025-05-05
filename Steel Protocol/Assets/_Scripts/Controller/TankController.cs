using UnityEngine;
using SteelProtocol.Input;
using SteelProtocol.Controller.Player;


namespace SteelProtocol.Controller
{
    public class TankController : MonoBehaviour
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Todo: Give me some time to think about this and either I'll make it shine or it'll be forgotten. //
        //                I know what I want to do, but I need to figure out how to do it.                  //
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        private PlInputBridge input;


        public void Awake()
        {
            input = GetComponent<PlInputBridge>();

            if (input == null)
                Debug.LogError("PlInputBridge not found.");
        }


        public void Update()
        {
            if (input.IsExiting())
            {
                Debug.Log("Exiting game...");

                Application.Quit();
            }
        }
    }

}
