using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ColliderVisualizer : MonoBehaviour
{
    public GameObject visualSphere;
    private SphereCollider sphereCollider;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();

        if (visualSphere != null)
        {
            // Match the position and scale
            visualSphere.transform.localPosition = sphereCollider.transform.localPosition;
            float diameter = sphereCollider.radius * 2f;
            visualSphere.transform.localScale = new Vector3(diameter, diameter, diameter);

            // Optional: make sure it's visible during play
            visualSphere.SetActive(true);
        }
    }
}
