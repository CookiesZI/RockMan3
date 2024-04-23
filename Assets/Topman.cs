using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topman : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform[] shootPoints; // Positions to shoot projectiles from
    public Transform player; // Reference to the player
    public float projectileSpeed = 10f;
    public float chargeSpeed = 5f;
    public float shootInterval = 3f;
    public float chargeDuration = 2f;

    private bool isCharging = false;

    void Start()
    {
        // Start the shooting state
        StartCoroutine(ShootState());
    }

    IEnumerator ShootState()
    {
        while (true)
        {
            // Shoot projectiles
            ShootProjectiles();

            // Wait for the shoot interval
            yield return new WaitForSeconds(shootInterval);

            // Start charging state
            yield return StartCoroutine(ChargeState()); // Start charging and wait for it to finish
        }
    }

    IEnumerator ChargeState()
    {
        isCharging = true;

        // Move towards the player
        while (Vector3.Distance(transform.position, player.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);
            yield return null;
        }

        isCharging = false;
    }

    void ShootProjectiles()
    {
        foreach (Transform shootPoint in shootPoints)
        {
            // Calculate direction towards the player
            Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;

            // Instantiate the projectile at the shoot point
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            // Get the Rigidbody component of the projectile
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // Apply force to the projectile in the direction towards the player
            projectileRb.velocity = directionToPlayer * projectileSpeed;
        }
    }
}
