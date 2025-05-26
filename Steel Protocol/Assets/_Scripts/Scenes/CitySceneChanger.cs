using UnityEngine;

namespace SteelProtocol.Scenes
{
    // Many thanks to MetalStorm Games for the awesome tutorial 
    // On how to easily change scenes in Unity
    // Unity Basics - How to switch Scenes (Levels) - https://www.youtube.com/watch?v=ztJPnBpae_0
    public class CitySceneChanger : MonoBehaviour
    {
        [SerializeField] private EnumScenes sceneToLoad;


        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Starts the LoadScene coroutine
                StartCoroutine(SceneChanger.LoadScene(sceneToLoad.ToString()));
            }
        }
    }
}
