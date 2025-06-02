using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Targeting
{
    public struct Target
    {
        private GameObject target;


        public Target(GameObject target)
        {
            this.target = target;
        }


        // Property to access the private fields
        public GameObject TargetObject { readonly get => target; set => target = value; }


        // HashSet magical stuff, override the Equals method to compare Target objects
        public override bool Equals(object obj)
        {
            if (obj is Target other)
            {
                return target == other.target;
            }
            return false;
        }

        // I don't even know man, this is just needed for the HashSet to work
        public override int GetHashCode()
        {
            return target != null ? target.GetHashCode() : 0;
        }
    }
    
}
