using System.Collections;
using SteelProtocol.Scenes;
using UnityEngine;

namespace SteelProtocol
{
    public class OutOfBounds : MonoBehaviour
    {
        private bool isOutOfBounds = false;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isOutOfBounds = true;
                StartCoroutine(CheckOutOfBounds());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isOutOfBounds = false;
                StopCoroutine(CheckOutOfBounds());
            }
        }

        IEnumerator CheckOutOfBounds()
        {
            float timer = 0f;

            while (timer < 3f)
            {
                if (!isOutOfBounds)
                {
                    yield break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(SceneChanger.ReloadCurrentScene());
        }
    }
}
