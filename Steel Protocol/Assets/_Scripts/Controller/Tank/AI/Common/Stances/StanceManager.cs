using UnityEngine;

namespace SteelProtocol.Controller.Tank.AI.Common.Stances
{
    public class StanceManager : MonoBehaviour
    {
        private AiStance currentStance;
        [SerializeField] private EnumStancesPrimary primaryStance;
        [SerializeField] private EnumStancesSecondary secondaryStance;

        public void SetStance(AiStance newStance)
        {
            if (currentStance != null)
                currentStance.OnStanceExit();

            currentStance = newStance;
            currentStance.OnStanceEnter();
        }

        private void Update()
        {
            if (currentStance != null)
                currentStance.OnStanceUpdate();
        }
    }
}
