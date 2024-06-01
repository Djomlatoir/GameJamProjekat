using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoints : MonoBehaviour
{

    public GameObject playerObj;
    public Transform player; // Reference to the player's transform
    private static Transform teleportTarget; // Reference to the teleport target's transform
    public Transform setCheckpoint;
    private bool isSetPoint = false;
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            setCheckpoint.gameObject.SetActive(true);
            teleportTarget = setCheckpoint;
            isSetPoint = true;
           // playerObj.SetActive(false);
           // TeleportPlayer();
              Debug.Log("TeleportStart");
            Debug.Log(player);
            Debug.Log(teleportTarget);
            Debug.Log(isSetPoint);


        }
    }

   public void TeleportPlayer()
    {
        playerObj.SetActive(false);
        if (player != null && teleportTarget != null && isSetPoint)
        {
            Debug.Log("TeleportStart deaths");
            // Debug.Log("Telleportt");
            player.position = teleportTarget.position; // Teleport the player to the teleport target's position
            playerObj.SetActive(true);
            setCheckpoint.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
