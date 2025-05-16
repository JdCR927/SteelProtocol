using UnityEngine;

namespace SteelProtocol.Controller
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target; // Player

        private Vector3 offset = new Vector3(0f, 45f, -45f);

        
        void LateUpdate()
        {
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
    }
}
