using System.Collections;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damage = 1;
    private bool isColliding = false;
    private HealthPlayer playerHealth;
    public float damageInterval = 0.7f;
    private float nextDamageTime = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  )
        {
            Debug.Log("HP dmg");
            playerHealth = other.gameObject.GetComponent<HealthPlayer>();
            isColliding = true;
            if (playerHealth.health >0)
            {
                StartCoroutine(DealDamageOverTime());
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (isColliding)
        {
            if (Time.time >= nextDamageTime && playerHealth.health >0)
            {
                playerHealth.PlayerTakeDamage(damage);
             //   Debug.Log("Started taking damage at damage= " + Time.time);
                nextDamageTime = Time.time + damageInterval;
                if(playerHealth.health <= 0)
                {
                    playerHealth.health = 0;
                    nextDamageTime = Time.time + damageInterval*10f;

                }
            }

            yield return null;
        }
    }
}
