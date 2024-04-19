using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 12;
    public int currenthealth;
    public GameObject player;
    public float kbForce;
    public GameObject checkpoint;

    private void Start()
    {
        currenthealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;
        Knockback();

        if(currenthealth <= 0)
        {
            player.transform.position = checkpoint.transform.position;
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

    void Knockback()
    {
        player.transform.position += transform.right * Time.deltaTime * kbForce;
    }
}
