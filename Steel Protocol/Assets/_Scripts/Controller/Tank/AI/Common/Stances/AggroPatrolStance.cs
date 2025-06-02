namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class AggroPatrolStance : AiStance
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

                if (sqrDistance >= rng * rng)
                {
                    mover.Pause = true;

                    ChargeTarget(target);
                }
                else
                {
                    mover.Pause = true;
                    movement.Move(0f, 0f);
                }
            }
            else
            {
                mover.Pause = false; // AiMover will resume only if there are no enemies left
            }
        }

        private void ChargeTarget(UnityEngine.GameObject target)
        {
            input.OnMove(target.transform.position);
            movement.Move(input.GetForwardInput(), input.GetTurnInput());
        }
    }
}
