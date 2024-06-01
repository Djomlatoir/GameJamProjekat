using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this line to access Unity's UI classes

public class HealthPlayer : MonoBehaviour
{
    public CheckPoints checkPoint;
   // public GameTimer gameTimer;
    public int health;
    public int maxHealth;

    public Slider healthSlider;
    public Slider healthSlider2d;// Reference to the Slider UI element
    private PlayerInput playerInput;

    private bool isDead = false;
    private float respawnTimer = 0f;
    public float respawnDelay = 3f;
    public GameObject deathScreen;

    public TextMeshProUGUI playerHealthTxt;
    public TextMeshProUGUI playerHealthTxt2d;

    public float invurnableTime = 3f;
    public bool isInvurnable = false;
    private bool isRespawning = false;



    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI(); // Update the health UI when the game starts
        playerInput = GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if (isDead)
        {
            deathScreen.SetActive(true);
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnDelay)
            {
                Respawn();
                isDead = false;
                respawnTimer = 0f;
              //  deathScreen.SetActive(false);
            }
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        if (health > 0 && !isRespawning && !isInvurnable)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0; // Ensure health doesn't go below zero
                isDead = true;
                StartCoroutine(InvurnableCooldown());
                //  Respawn();
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }
                //  gameTimer.GameEnd();
            }
            UpdateHealthUI(); // Update the health UI after taking damage
        }
        
    }

    private void Respawn()
    {
        isRespawning = true;

       

        checkPoint.TeleportPlayer();
        health = maxHealth;
        UpdateHealthUI();
        if (playerInput != null)
        {
            playerInput.enabled = enabled;
        }
        isDead = false;
       
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isRespawning=false;
        isInvurnable = false;
        Debug.Log(isInvurnable);
    }

    public IEnumerator InvurnableCooldown()
    {
        
        isInvurnable = true;
        yield return new WaitForSeconds(invurnableTime);
        isInvurnable = false;


    }

    private void UpdateHealthUI()
    {
        if (playerHealthTxt != null)
            playerHealthTxt.text = health.ToString();
        

        if (healthSlider != null)
            healthSlider.value = (float)health / maxHealth; // Update the slider value based on current health

        if (playerHealthTxt2d != null)
            playerHealthTxt2d.text = health.ToString();
        if (healthSlider2d != null)
            healthSlider2d.value = (float)health / maxHealth;
    }
}
