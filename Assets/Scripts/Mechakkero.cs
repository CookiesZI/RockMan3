using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechakkero : MonoBehaviour
{
    public Transform playerTransform;
    public float forwardSpeed = 5f; // Speed of forward movement
    public float jumpForce = 10f;
    public float jumpInterval = 2f; // Time between jumps
    public float landingTime = 1f; // Time the enemy stays on the floor after a jump
    public LayerMask groundLayer; // LayerMask for the ground

    private Rigidbody rb;
    Animator ab;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        InvokeRepeating("JumpAndMoveTowardsPlayer", 0f, jumpInterval);
    }

    void JumpAndMoveTowardsPlayer()
    {
        if (!isJumping && playerTransform != null)
        {
            isJumping = true;

            // Calculate direction to player
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f; // Make sure the enemy doesn't jump up or down

            // Add force to move forward
            rb.velocity = transform.forward * forwardSpeed;

            // Add force to jump towards the player
            rb.AddForce(directionToPlayer.normalized * jumpForce, ForceMode.VelocityChange);

            ab.SetTrigger("Jump");

            Invoke("FinishJump", landingTime);
        }
    }

    void FinishJump()
    {
        isJumping = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object on the ground layer
        if ((groundLayer & 1 << collision.gameObject.layer) != 0)
        {
            rb.velocity = Vector3.zero; // Stop horizontal movement on landing
        }
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playercomponant))
        {
            playercomponant.TakeDamage(1);
        }
    }
}
