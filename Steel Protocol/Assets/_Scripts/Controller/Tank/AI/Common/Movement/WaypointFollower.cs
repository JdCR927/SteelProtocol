using UnityEngine;
using SteelProtocol.Controller.Tank.Common.Movement;
using System.Collections;

namespace SteelProtocol.Controller.Tank.AI.Common.Movement
{
    public class WaypointFollower : MonoBehaviour
    {
        private AiInputBridge input;
        private MovementController movement;
        [SerializeField] private Vector3[] waypoints;

        private int currentIndex = 0;
        private readonly float waypointThreshold = 1f;

        private readonly float movementTickRate = 0.001f; // Tick rate for AI thinking
        private Coroutine movementRoutine;

        public void Awake()
        {
            input = GetComponent<AiInputBridge>();
            movement = GetComponent<MovementController>();
        }

        private void Start()
        {
            movementRoutine = StartCoroutine(WaypointLoop());
        }

        private void OnDisable()
        {
            if (movementRoutine != null)
                StopCoroutine(movementRoutine);
        }

        private IEnumerator WaypointLoop()
        {
            WaitForSeconds wait = new WaitForSeconds(movementTickRate);

            while (true)
            {
                Patrol(); // 👈 modular call

                yield return wait;
            }
        }

        private void Patrol()
        {
            if (waypoints.Length == 0) return;

            Vector3 currentTarget = waypoints[currentIndex];

            input.OnMove(currentTarget);

            movement.Move(input.GetForwardInput(), input.GetTurnInput());

            float sqrDistance = (transform.position - currentTarget).sqrMagnitude;
            if (sqrDistance < waypointThreshold)
            {
                currentIndex = (currentIndex + 1) % waypoints.Length; // loop around
            }
        }

    }
}
