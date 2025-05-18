using SteelProtocol.Input;
using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Turret;

namespace SteelProtocol.Controller.Tank.Player
{
    [RequireComponent(typeof(TurretController))]
    [RequireComponent(typeof(PlInputBridge))]
    public class PlTurretController: MonoBehaviour
    {
        private IInputSource input;
        private TurretController aiming;
        

        private void Awake()
        {
            // Get the input source and aiming controller components
            input = GetComponent<IInputSource>();
            aiming = GetComponent<TurretController>();

        }

        private void FixedUpdate()
        {
            aiming.Aim(input.GetLookInput());
        }

    }
}