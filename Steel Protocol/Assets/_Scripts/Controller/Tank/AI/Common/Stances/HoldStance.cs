namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class HoldStance : AiStance
    {
        public override void OnStanceEnter()
        {
            mover.Loop = false;
            mover.Pause = false;
        }

        public override void OnStanceExit()
        {
            if (mover != null)
            {
                mover.Pause = true;
            }
        }

        public override void OnStanceUpdate()
        {
            var target = detection.GetClosestTarget();

            if (target != null)
            {
                mover.Pause = true;
                movement.Move(0f, 0f); // Let MovementController slow down naturally
            }
            else
            {
                mover.Pause = false; // AiMover will resume *only* if it hasn’t finished
            }
        }
    }
}
