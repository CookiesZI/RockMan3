using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetallDx : MonoBehaviour
{
    Animator ab;
    Rigidbody rb;

    public float lineOfSite;
    private Transform player;

    //Shoot
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float speed = 3;

    private float fireRate = 2f;
    private float nextFire = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ab = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && Time.time > nextFire)
        {
            ShootPlayer();
            ab.SetTrigger("dxAttack");
        }
    }
    void ShootPlayer()
    {
        nextFire = Time.time + fireRate;
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Apply force to the projectile in the direction towards the player
        projectileRb.velocity = directionToPlayer * speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
