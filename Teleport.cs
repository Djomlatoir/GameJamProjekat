using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject playerObj;
    public Transform player; // Reference to the player's transform
    public Transform teleportTarget; // Reference to the teleport target's transform

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {   
            playerObj.SetActive(false);
            TeleportPlayer();
          //  Debug.Log("TeleportStart");
        }
    }

    void TeleportPlayer()
    {
        if (player != null && teleportTarget != null)
        {
           // Debug.Log("Telleportt");
            player.position = teleportTarget.position; // Teleport the player to the teleport target's position
            playerObj.SetActive(true);
        }
    }
}
