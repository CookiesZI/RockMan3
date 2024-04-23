using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    Animator ab;

    private void Start()
    {
        ab = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamageE(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
