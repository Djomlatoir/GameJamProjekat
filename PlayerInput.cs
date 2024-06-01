using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject pauzaUI;
    public bool pauza = false;
    public CharacterController characterController;
    public float maxSpeed = 12f;
    public float acceleration = 5f; // Acceleration value
    public float deceleration = 5f; // Deceleration value
    public float jumpHeight = 3f; // Height of the jump
    public float gravity = -9.81f; // Gravity value
    public float jumpBonus = 2f; // Bonus acceleration when jumping perfectly upon hitting the ground
    public float jumpBonusDecreaseRate = 2f; // Rate at which jump bonus decreases when the jump timing is not optimal
    public float maxHoldTime = 1f; // Maximum time to hold the jump button
    public AnimationCurve jumpCurve; // Curve for modifying jump height while the button is held

    private Vector3 velocity; // Player's current velocity
    private float currentSpeed; // Current speed of the player
    private bool isJumping = false; // Whether the player is currently jumping
    private float jumpBonusValue; // Current value of the jump bonus
    private float jumpStartTime; // Time when the jump button was pressed

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauza = !pauza;
            if(pauza)
            {
                Cursor.lockState = CursorLockMode.None;
               // Debug.Log("Pauza");
                Time.timeScale = 0f;
                pauzaUI.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
              //  Debug.Log("resume");
                Time.timeScale = 1f;
                pauzaUI.SetActive(false);
            }

        }
       
        // Apply acceleration
        if (move.magnitude > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement
        characterController.Move((move.normalized * currentSpeed + velocity) * Time.deltaTime);

        // Reset vertical velocity if player is grounded
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;

            if (isJumping)
            {
                currentSpeed += jumpBonusValue;
                isJumping = false;
            }
            jumpBonusValue = 0f;
        }

        // Check for jump input
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            // Calculate jump velocity based on jumpHeight
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            jumpBonusValue = jumpBonus;
            jumpStartTime = Time.time;
        }
        else if (Input.GetButton("Jump") && isJumping && (Time.time - jumpStartTime) < maxHoldTime)
        {
            // Modify jump height while the jump button is held down
            float jumpTime = Time.time - jumpStartTime;
            float normalizedJumpTime = jumpTime / maxHoldTime;
            float modifiedJumpHeight = jumpHeight * jumpCurve.Evaluate(normalizedJumpTime);
            velocity.y = Mathf.Sqrt(modifiedJumpHeight * -2f * gravity);
        }
        else if (isJumping && !characterController.isGrounded)
        {
            // Decrease jump bonus if the player is in the air after jumping
            jumpBonusValue -= jumpBonusDecreaseRate * Time.deltaTime;
            jumpBonusValue = Mathf.Clamp(jumpBonusValue, 0f, jumpBonus);
        }
    }
}
