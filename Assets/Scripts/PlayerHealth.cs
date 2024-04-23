using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator ab;

    public int maxHealth = 12;
    public int currenthealth;

    [SerializeField] HealthBar healthBar;
    private void Start()
    {
        currenthealth = maxHealth;
        healthBar.UpdateHealthBar(currenthealth, maxHealth);
    }

    private void Awake()
    {
        healthBar = GetComponent<HealthBar>();
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;
        healthBar.UpdateHealthBar(currenthealth, maxHealth);

        if(currenthealth <= 0)
        {
            ab.SetTrigger("Trigger");
        }
    }

    public void Heal(int amount)
    {
        currenthealth += amount;

        if (currenthealth > maxHealth)
        {
            currenthealth = maxHealth;
        }
    }
}
