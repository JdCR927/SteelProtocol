using UnityEngine;

namespace SteelProtocol.Controller.Tank.Common.Movement
{
    public class TrackMovementController : MonoBehaviour
    {
        private Transform leftTrack;
        private Transform rightTrack;
        private Transform leftWheels;
        private Transform rightWheels;

        private MovementController movement;
        private float currentSpeed;
        private float offset = 0f;


        void Awake()
        {
            leftTrack = GetComponent<TankController>().LeftTrack;
            rightTrack = GetComponent<TankController>().RightTrack;
            leftWheels = GetComponent<TankController>().LeftWheels;
            rightWheels = GetComponent<TankController>().RightWheels;

            movement = GetComponent<MovementController>();

            if (movement == null)
                Debug.LogError("MovementController not found on the object!");
        }


        void FixedUpdate()
        {
            currentSpeed = movement.GetCurrentSpeed();

            RotateTracks(leftTrack, currentSpeed);
            RotateTracks(rightTrack, currentSpeed);
            RotateWheels(leftWheels, currentSpeed);
            RotateWheels(rightWheels, currentSpeed);
        }


        // Many thanks to reignamation from the Unity Forums for suggesting the change of "_MainTex" to "_BaseMap"
        // What a legend
        // https://discussions.unity.com/t/kinda-solved-cant-change-texture-offset-via-script-in-urp/757393/14
        private void RotateTracks(Transform track, float speed)
        {
            if (track == null)
            {
                Debug.LogError("Track not assigned!");
                return;
            }

            if (!track.TryGetComponent<Renderer>(out var renderer))
            {
                Debug.LogError("Renderer not found on the track!");
                return;
            }

            var material = renderer.material;
            if (material == null)
            {
                Debug.LogError("Material not found on the renderer!");
                return;
            }

            offset += speed * Time.deltaTime;

            material.SetTextureOffset("_BaseMap", new Vector2(0, -offset));
        }

        private static void RotateWheels(Transform wheels, float speed)
        {
            if (wheels == null)
            {
                Debug.LogError("Wheels not assigned!");
                return;
            }

            foreach (Transform wheel in wheels)
            {
                wheel.Rotate(Vector3.right, speed);
            }
        }
    }
}

