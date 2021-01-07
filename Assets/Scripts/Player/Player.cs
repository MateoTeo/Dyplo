using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public Flash flash;
    public Slider healthBar;
    PlayerMovement plm;
    public bool isDead = false;

    private void Start()
    {
        plm = GetComponent<PlayerMovement>();
    }

    private void Awake()
    {        
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        flash.FlashDamage();

        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;
            maxHealth = 0;
            isDead = true;
            plm.enabled = false;
            Invoke("DeadScene", 3f);
        }
    }

    public void TakeFirstKit(float health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    private void DeadScene()
    {
        SceneManager.LoadScene("Lose");
    }   
}
