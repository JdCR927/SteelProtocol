using System;
using UnityEngine;

namespace SteelProtocol.Mission
{
    public class WaveTracker : MonoBehaviour
    {
        private GameObject[] currentWave;
        public event Action<WaveTracker> OnWaveCleared;

        private bool isCleared = false;

        private void Update()
        {
            if (isCleared) return;

            bool allDead = true;
            foreach (var tank in currentWave)
            {
                if (tank != null)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead)
            {
                isCleared = true;
                OnWaveCleared?.Invoke(this);
                Destroy(gameObject);
            }
        }

        public void Initialize(GameObject[] wave)
        {
            currentWave = wave;
        }
    }
}
