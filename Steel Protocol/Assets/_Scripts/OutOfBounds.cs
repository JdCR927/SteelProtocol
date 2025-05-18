using System.Collections;
using UnityEngine;

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
        else if (other.CompareTag("Friend") || other.CompareTag("Enemy"))
        {
            // TODO: Teleport to spawn point
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

        while (timer < 5f)
        {
            if (!isOutOfBounds)
            {
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // TODO: Do something when out of bounds for 5 seconds
        // Kill player or teleport to spawn point, whatever seems fit
    }
}
