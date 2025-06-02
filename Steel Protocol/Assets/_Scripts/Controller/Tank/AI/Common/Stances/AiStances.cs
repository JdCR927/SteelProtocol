using UnityEngine;
using SteelProtocol.Controller.Tank.AI.Common.FCS;
using SteelProtocol.Controller.Tank.Common.Movement;
using SteelProtocol.Controller.Tank.AI.Common.Movement;
using SteelProtocol.Controller.Tank.AI.Common.Targeting;

namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public abstract class AiStance
    {
        protected AiInputBridge input;
        protected MovementController movement;
        protected FiringControlSystem fcs;
        protected AiMover mover;
        protected DetectionTrigger detection;

        public virtual void Initialize(AiInputBridge input, MovementController movement, FiringControlSystem fcs, AiMover mover, DetectionTrigger detection)
        {
            this.input = input;
            this.movement = movement;
            this.fcs = fcs;
            this.mover = mover;
            this.detection = detection;
        }

        public abstract void OnStanceUpdate(); // Called in Update
        public abstract void OnStanceEnter();  // Called when switching TO this stance
        public abstract void OnStanceExit();   // Called when switching FROM this stance
    }
}
