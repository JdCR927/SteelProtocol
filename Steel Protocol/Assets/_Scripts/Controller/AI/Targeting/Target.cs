using UnityEngine;

namespace SteelProtocol.Controller.AI.Targeting
{
    public struct Target
    {
        private GameObject target;
        private Ray raycast;
        private float distance;

        public Target(GameObject target, Ray raycast, float distance)
        {
            this.target = target;
            this.raycast = raycast;
            this.distance = distance;
        }

        // Properties to access the private fields
        public GameObject TargetObject { readonly get => target; set => target = value; }

        public Ray Raycast { readonly get => raycast; set => raycast = value; }

        public float Distance { readonly get => distance; set => distance = value; }


        // HashSet magical stuff, used to compare things

        // Override the Equals method to compare Target objects
        public override bool Equals(object obj)
        {
            if (obj is Target other)
            {
                return target == other.target;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return target != null ? target.GetHashCode() : 0;
        }
    }
    
}
