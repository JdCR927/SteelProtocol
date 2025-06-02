using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class PatrolStance : AiStance
    {
        [SerializeField] private Vector3[] waypoints;
        private int currentIndex = 0;
        private float threshold = 1f;

        public override void OnStanceUpdate()
        {
            if (waypoints.Length == 0) return;

            Vector3 target = waypoints[currentIndex];
            input.OnMove(target);
            movement.Move(input.GetForwardInput(), input.GetTurnInput());

            if (Vector3.Distance(transform.position, target) < threshold)
            {
                currentIndex = (currentIndex + 1) % waypoints.Length;
            }
        }

        public override void OnStanceEnter() { }
        public override void OnStanceExit() { }
    }
}
