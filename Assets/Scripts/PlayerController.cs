using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator ab;
    [SerializeField] float speed;

    float inputHorizontal;
    bool facingRight;
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] float jumpForce;

    public Transform firepoint;
    public GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shooting();
        }
    }

    void FixedUpdate()
    {
        Jump();
        Move();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    void Move()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        ab.SetFloat("Speed", Mathf.Abs(inputHorizontal));
        if (inputHorizontal != 0)
        {
            rb.velocity = new Vector3(inputHorizontal * speed, rb.velocity.y, 0);
        }
        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (inputHorizontal < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

    }

    void Shooting()
    {
        GameObject bullets = Instantiate(bulletPrefab, firepoint.transform.position, firepoint.transform.rotation);
    }

}
