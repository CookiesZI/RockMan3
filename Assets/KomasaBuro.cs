using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomasaBuro : MonoBehaviour
{
    Rigidbody rb;
    Animator ab;

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootSpeed = 10f;

    public float lineOfSite;
    private Transform player;

    private float fireRate = 4f;
    private float nextFire = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && Time.time > nextFire)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        nextFire = Time.time + fireRate;
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Get the direction to shoot in (same as the shooting object's forward direction)
        Vector3 shootDirection = transform.forward;
        projectileRb.AddForce(shootDirection * shootSpeed, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
