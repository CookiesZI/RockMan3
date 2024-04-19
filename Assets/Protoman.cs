using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protoman : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    bool isfacingRight;

    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] float jumpForce;

    Rigidbody rb;
    Animator ab;

    public int currentAmmo;
    public int maxAmmo = 2;

    public float arrivalThreshold = 0.1f;
    public GameObject bulletPrefab;
    public Transform firepoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        targetPoint = 0;
        isfacingRight = false;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, patrolPoints[targetPoint].position);

        if (distance <= arrivalThreshold)
        {
            increaseTargetInt();
        }

        MoveTowardsPatrolPoint();

        Invoke("Jump", 1);
        Shoot();
        Invoke("Reload", 3);
    }

    void MoveTowardsPatrolPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }

        Flip();
    }

    void Flip()
    {
        isfacingRight = !isfacingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

     void Shoot()
    {   if (currentAmmo > 0)
        {
            GameObject bullets = Instantiate(bulletPrefab, firepoint.transform.position, firepoint.transform.rotation);
            currentAmmo--;
        }
    }

    void Jump()
    {
        if (grounded)
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;
    }

    void Reload()
    {
        int reloadAmount = maxAmmo - currentAmmo;
        currentAmmo += reloadAmount;
    }
}
