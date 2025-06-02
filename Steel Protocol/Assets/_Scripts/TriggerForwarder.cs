using UnityEngine;
using SteelProtocol.Controller.Tank.AI.Common.Targeting;

namespace SteelProtocol
{
    public class TriggerForwarder : MonoBehaviour
    {
        private DetectionTrigger detectionScript;

        private void Awake()
        {
            var internalScripts = transform.parent.Find("_InternalScripts");

            detectionScript = internalScripts.GetComponent<DetectionTrigger>();
        }

        private void OnTriggerEnter(Collider other)
        {
            detectionScript?.SendMessage("OnTriggerEnter", other, SendMessageOptions.DontRequireReceiver);
        }

        private void OnTriggerExit(Collider other)
        {
            detectionScript?.SendMessage("OnTriggerExit", other, SendMessageOptions.DontRequireReceiver);
        }
    }
}
