using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
    public float bounceForce = 10f; // Adjust this value to control the strength of the bounce

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has a CharacterController component
        CharacterController characterController = other.GetComponent<CharacterController>();
        if (characterController != null)
        {
            // Apply the bounce force to the character controller
            characterController.Move(Vector3.up * bounceForce * Time.deltaTime);
        }
    }
}
