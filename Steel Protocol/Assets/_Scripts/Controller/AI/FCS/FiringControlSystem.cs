using System.Collections;
using UnityEngine;

namespace SteelProtocol.Controller.AI.FCS
{
    // Credits for the whole coroutine thing go to the following for explaining:
    // Answer of Max Yankov (05/05/2015) - https://stackoverflow.com/a/30057705
    public class FiringControlSystem : MonoBehaviour
    {
        // Interface for input handling
        private AiInputBridge input;

        private bool attackFlag = false;

        private Coroutine attackCheckCoroutine;

        private bool isCheckingAttack = false;


        public void Awake()
        {
            input = GetComponent<AiInputBridge>();

            if (input == null)
                Debug.LogError("Missing AiInputBridge component.");
        }

        public void FireWeapons(bool flag)
        {
            attackFlag = flag;

            if (attackFlag && !isCheckingAttack)
            {
                attackCheckCoroutine = StartCoroutine(CheckAttackDuration());
            }

            if (!attackFlag && isCheckingAttack)
            {
                StopCoroutine(attackCheckCoroutine);
                isCheckingAttack = false;
            }
        }


        private IEnumerator CheckAttackDuration()
        {
            isCheckingAttack = true;

            float timer = 0f;

            while (timer < 5f)
            {

                if (!attackFlag)
                {
                    isCheckingAttack = false;
                    yield break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            AttackMain();

            isCheckingAttack = false;
        }


        public void AttackMain()
        {
            // Simulate attack button press
            input.OnAttack1(true);

            // Simulate release on next frame
            StartCoroutine(ReleaseAttack1());
        }

        private IEnumerator ReleaseAttack1()
        {
            yield return null; // Wait one frame
            input.OnAttack1(false);
        }

        /*
        public void AttackSecond()
        {

        }


        public void AttackThird()
        {

        }
        */
    }
}
