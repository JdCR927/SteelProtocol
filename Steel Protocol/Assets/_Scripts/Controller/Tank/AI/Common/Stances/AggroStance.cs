namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class AggroStance : AiStance
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
            throw new System.NotImplementedException();
        }
    }
}
