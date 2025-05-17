using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SteelProtocol
{
    public class SceneChanger : Singleton<SceneChanger>
    {
        [SerializeField] private Animator transition;


        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            AnimatorStateInfo currentState = transition.GetCurrentAnimatorStateInfo(0);
            if (!currentState.IsName("Crossfade_End"))
            {
                transition.SetTrigger("End");
            }
        }


        // Many thanks to Brackeys for the great tutorial
        // On transition animations for Unity
        // How to make AWESOME Scene Transitions in Unity! - https://www.youtube.com/watch?v=CE9VOZivb3I
        public static IEnumerator LoadScene(string sceneName)
        {
            AnimatorStateInfo currentState = Instance.transition.GetCurrentAnimatorStateInfo(0);
            if (!currentState.IsName("Crossfade_Start"))
            {
                // Play the animation
                Instance.transition.SetTrigger("Start");

                // Wait for the animation to finish
                yield return new WaitForSeconds(3);

                // Load the scene
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
