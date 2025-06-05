using System;
using UnityEngine;

namespace SteelProtocol.Mission
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] firstWave;
        [SerializeField] private GameObject[] secondWave;
        [SerializeField] private GameObject[] thirdWave;

        private int waveIndex = 0;

        public event Action OnAllWavesCleared;

        void Start()
        {
            CreateTracker(firstWave);
        }


        private void CreateTracker(GameObject[] wave)
        {
            var trackerObj = new GameObject("WaveTracker_" + wave[0].name);
            var tracker = trackerObj.AddComponent<WaveTracker>();
            tracker.Initialize(wave);
            tracker.OnWaveCleared += OnWaveCleared;
        }


        private void OnWaveCleared(WaveTracker tracker)
        {
            var nextWave = GetWaveByIndex(waveIndex + 1);

            if (nextWave != null)
            {
                waveIndex++;
                SetWaveActive(nextWave, true);
                CreateTracker(nextWave);
            }
            else
            {
                OnAllWavesCleared?.Invoke();
            }
        }

        private void SetWaveActive(GameObject[] wave, bool active)
        {
            foreach (var tank in wave)
            {
                if (tank != null)
                    tank.SetActive(active);
            }
        }


        private GameObject[] GetWaveByIndex(int index)
        {
            return index switch
            {
                0 => firstWave,
                1 => secondWave,
                2 => thirdWave,
                _ => null
            };
        }
    }

}
