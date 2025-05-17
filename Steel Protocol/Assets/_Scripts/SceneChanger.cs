using UnityEngine;

namespace SteelProtocol
{
    // Many thanks to MetalStorm Games for the awesome tutorial 
    // On how to easily change scenes in Unity
    // Unity Basics - How to switch Scenes (Levels) - https://www.youtube.com/watch?v=ztJPnBpae_0
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private EnumScenes sceneToLoad;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Load the next scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad.ToString());
            }
        }
    }
}
