using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float moveSpeed = 1.5f;

    private Rigidbody rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            rb.velocity = transform.right * moveSpeed;
        }
        else
        {
            rb.velocity = -transform.right * moveSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Change direction
            movingRight = !movingRight;
        }
    }
}
