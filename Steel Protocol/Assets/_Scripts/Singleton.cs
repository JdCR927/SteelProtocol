using UnityEngine;

namespace SteelProtocol
{
    /// <summary>
    /// Modified version of the following Singleton script for Unity.
    /// Allows other scripts to use the Singleton pattern without repeating code.
    /// <see href="https://awesometuts.com/blog/singletons-unity#elementor-toc__heading-anchor-2"/>
    /// </summary>
    /// <typeparam name="T">Generic script e.g. InputManager</typeparam> <summary>
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
