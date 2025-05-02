using UnityEngine;

namespace SteelProtocol.Controller
{
    public class TrackMovementController : MonoBehaviour
    {
        [SerializeField] private Transform leftTrack;
        [SerializeField] private Transform rightTrack;
        [SerializeField] private Transform leftWheels;
        [SerializeField] private Transform rightWheels;

        private MovementController movement;
        private float currentSpeed;
        private float offset = 0f;


        void Awake()
        {
            if (leftTrack == null || rightTrack == null)
                Debug.LogError("Left or right track not assigned!");

            if (leftWheels == null || rightWheels == null)
                Debug.LogError("Left or right wheels not assigned!");

            movement = GetComponent<MovementController>();

            if (movement == null)
                Debug.LogError("MovementController not found on the object!");
        }


        void FixedUpdate()
        {
            currentSpeed = movement.GetCurrentSpeed();

            rotateTracks(leftTrack, currentSpeed);
            rotateTracks(rightTrack, currentSpeed);
            rotateWheels(leftWheels, currentSpeed);
            rotateWheels(rightWheels, currentSpeed);
        }


        // Many thanks to reignamation from the Unity Forums for suggesting the change of "_MainTex" to "_BaseMap"
        // What a legend
        // https://discussions.unity.com/t/kinda-solved-cant-change-texture-offset-via-script-in-urp/757393/14
        private void rotateTracks(Transform track, float speed)
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

        private void rotateWheels(Transform wheels, float speed)
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

