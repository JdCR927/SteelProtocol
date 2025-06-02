namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class HoldPatrolStance : AiStance
    {
        public override void OnStanceEnter()
        {
            mover.Loop = true;
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
                float sqrDistance = (target.transform.position - rootTransform.position).sqrMagnitude;

                // Changes slightly from HoldStance, stopping from further away.
                // Look, this is just a hack because at 200 units, the shells
                // they fire fall just centimeters from hitting the enemies.
                if (sqrDistance <= 190f * 190f)
                {
                    mover.Pause = true;
                    movement.Move(0f, 0f); // Let MovementController slow down naturally
                }
                else
                {
                    mover.Pause = false;
                }
            }
            else
            {
                mover.Pause = false; // AiMover will resume only if there are no enemies left
            }
        }
    }
}
