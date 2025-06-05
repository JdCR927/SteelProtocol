using UnityEngine;
using SteelProtocol.Mission;

namespace SteelProtocol
{
    public class MissionManager : MonoBehaviour
    {
        [SerializeField] private Respawner respawner;
        [SerializeField] private GameObject player;

        private bool missionEnd = false;

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
        }

        private void HandleDefeat()
        {
            missionEnd = true;
        }
    }
}
