using UnityEngine;
using SteelProtocol.Mission;
using System;

namespace SteelProtocol
{
    public class MissionManager : MonoBehaviour
    {
        [SerializeField] private Respawner respawner;
        [SerializeField] private GameObject player;

        private bool missionEnd = false;

        public event Action OnVictory;
        public event Action OnDefeat;

        private void Start()
        {
            if (respawner != null)
                respawner.OnAllWavesCleared += HandleVictory;
        }

        private void Update()
        {
            if (missionEnd) return;

            if (player == null) HandleDefeat();
        }


        private void HandleVictory()
        {
            missionEnd = true;

            OnVictory?.Invoke();
        }

        private void HandleDefeat()
        {
            missionEnd = true;

            OnDefeat?.Invoke();
        }
    }
}
