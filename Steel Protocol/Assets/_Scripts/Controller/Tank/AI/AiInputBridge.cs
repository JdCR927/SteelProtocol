using UnityEngine;
using SteelProtocol.Input;
using SteelProtocol.Controller.Tank.AI.Processing;

namespace SteelProtocol.Controller.Tank.AI
{
    public class AiInputBridge : MonoBehaviour, IInputSource
    {
        private readonly AiMovementProcessor moveProc = new AiMovementProcessor();
        private readonly AiTurretProcessor turretProc = new AiTurretProcessor();

        private Transform root;
        private Transform turret;
        private Transform muzzle;

        private Vector2 movementInput;
        private Vector2 lookInput;
        private bool fireMain;


        public void Awake()
        {
            root = GetComponent<TankController>().Tank;
            turret = GetComponent<TankController>().Turret;
            muzzle = GetComponent<TankController>().Muzzle;
        }

        public void OnMove(Vector3 waypoint)
        {
            Vector3 from = root.position;
            Vector3 forward = root.forward;

            movementInput.x = moveProc.CalculateForwardInput(from, waypoint, forward);
            movementInput.y = moveProc.CalculateTurnInput(from, waypoint, forward);
        }


        public void OnLook(GameObject target)
        {
            if (target == null) return;

            Vector3 tgtPos = target.transform.position;

            lookInput.x = turretProc.CalculateYawDelta(turret.position, turret.forward, tgtPos);
            lookInput.y = turretProc.CalculatePitchDelta(root.position, muzzle.localEulerAngles, tgtPos);
        }


        public void OnAttack1(bool fire) => fireMain = fire;


        public float GetForwardInput() => movementInput.x;
        public float GetTurnInput() => movementInput.y;
        public Vector2 GetLookInput() => lookInput;
        public bool IsFiringMain() => fireMain;
    }
}
