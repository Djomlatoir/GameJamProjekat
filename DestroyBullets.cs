using System.Collections;
using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    public float delay = 2f; // Delay in seconds before destroying the object

    void Start()
    {
        // Start the coroutine to destroy the object after the specified delay
        StartCoroutine(DestroyObjectDelayed());
    }

   public  IEnumerator DestroyObjectDelayed()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the object after the delay
        Destroy(gameObject);
    }
}
