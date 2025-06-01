using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Movement;

namespace SteelProtocol.Controller.Tank.AI.Common.Movement
{
    public class WaypointFollower : MonoBehaviour
    {
        private AiInputBridge input;
        private MovementController movement;
        [SerializeField] private Vector3[] waypoints;

        private int currentIndex = 0;
        private readonly float waypointThreshold = 1f;

        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            movement = GetComponent<MovementController>();
        }

        public void FixedUpdate()
        {
            if (waypoints.Length > 0)
            {
                //MoveTo();
                Patrol();
            }
        }

        private void MoveTo()
        {
            Vector3 waypoint = waypoints[currentIndex];

            input.OnMove(waypoint);

            movement.Move(input.GetForwardInput(), input.GetTurnInput());
        }

        private void Patrol()
        {
            Vector3 currentTarget = waypoints[currentIndex];

            input.OnMove(currentTarget);

            movement.Move(input.GetForwardInput(), input.GetTurnInput());

            float distance = Vector3.Distance(transform.position, currentTarget);
            if (distance < waypointThreshold)
            {
                currentIndex = (currentIndex + 1) % waypoints.Length; // loop around
            }
        }

    }
}
