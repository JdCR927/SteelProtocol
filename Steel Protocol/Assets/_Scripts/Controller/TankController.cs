using UnityEngine;
using SteelProtocol.Input;


namespace SteelProtocol.Controller
{
    public class TankController : MonoBehaviour
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Todo: Give me some time to think about this and either I'll make it shine or it'll be forgotten. //
        //                I know what I want to do, but I need to figure out how to do it.                  //
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        private IInputSource input;


        public void Awake()
        {
            input = GetComponent<IInputSource>();

            if (input == null)
                Debug.LogError("IInputSource not found.");
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
