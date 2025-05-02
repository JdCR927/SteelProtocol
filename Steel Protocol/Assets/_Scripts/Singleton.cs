using UnityEngine;

namespace SteelProtocol
{
    // Credits for this Singleton template goes to: https://awesometuts.com/blog/singletons-unity#elementor-toc__heading-anchor-2
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
