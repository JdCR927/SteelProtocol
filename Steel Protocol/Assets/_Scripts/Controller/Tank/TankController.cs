using UnityEngine;

namespace SteelProtocol.Controller.Tank
{
    public class TankController : MonoBehaviour
    {
        [SerializeField] private Transform tank;
        [SerializeField] private Transform body;
        [SerializeField] private Transform turret;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private Transform leftTrack;
        [SerializeField] private Transform rightTrack;
        [SerializeField] private Transform leftWheels;
        [SerializeField] private Transform rightWheels;


        public Transform Tank {get => tank; set => tank = value; }
        public Transform Body { get => body; set => body = value; }
        public Transform Turret { get => turret; set => turret = value; }
        public Transform Muzzle { get => muzzle; set => muzzle = value; }
        public Transform FiringPoint { get => firingPoint; set => firingPoint = value; }
        public Transform LeftTrack { get => leftTrack; set => leftTrack = value; }
        public Transform RightTrack { get => rightTrack; set => rightTrack = value; }
        public Transform LeftWheels { get => leftWheels; set => leftWheels = value; }
        public Transform RightWheels { get => rightWheels; set => rightWheels = value; }
    }
}
