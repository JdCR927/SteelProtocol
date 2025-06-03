using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Movement;
using System.Collections;

namespace SteelProtocol.Controller.Tank.AI.Common.Movement
{
    public class AiMover : MonoBehaviour
    {
        private AiInputBridge input;
        private MovementController movement;
        private Vector3[] waypoints;

        private int currentIndex = 0;
        private readonly float waypointThreshold = 2f;
        private bool reachedFinalWaypoint = false;

        [HideInInspector] public bool Pause { get; set; }
        [HideInInspector] public bool Loop { get; set; }

        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            movement = GetComponent<MovementController>();
        }

        private void FixedUpdate()
        {
            if (Pause) return;
            if (waypoints.Length == 0 || reachedFinalWaypoint)
            {
                movement.Move(0f, 0f);
                return;
            }

            Vector3 currentTarget = waypoints[currentIndex];
            input.OnMove(currentTarget);
            movement.Move(input.GetForwardInput(), input.GetTurnInput());

            float sqrDistance = (transform.position - currentTarget).sqrMagnitude;
            if (sqrDistance < waypointThreshold * waypointThreshold)
            {
                currentIndex++;
                if (currentIndex >= waypoints.Length)
                {
                    if (Loop)
                        currentIndex = 0;
                    else
                        reachedFinalWaypoint = true;
                }
            }
        }

        public void SetWaypoints(Vector3[] waypointArr)
        {
            waypoints = waypointArr;
        }
    }
}
