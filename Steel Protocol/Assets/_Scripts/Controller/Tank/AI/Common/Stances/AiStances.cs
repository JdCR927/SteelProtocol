using UnityEngine;
using SteelProtocol.Controller.Tank.AI.Common.FCS;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public abstract class AiStance : MonoBehaviour
    {
        protected AiInputBridge input;
        protected MovementController movement;
        protected FiringControlSystem fcs;

        protected virtual void Awake()
        {
            input = GetComponent<AiInputBridge>();
            movement = GetComponent<MovementController>();
            fcs = GetComponent<FiringControlSystem>();
        }

        public abstract void OnStanceUpdate(); // Called in Update
        public abstract void OnStanceEnter();  // Called when switching TO this stance
        public abstract void OnStanceExit();   // Called when switching FROM this stance
    }
}
