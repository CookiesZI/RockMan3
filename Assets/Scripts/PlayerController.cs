using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody rb;
    Animator ab;

    bool facingRight;

    //movement
    public float speed;
    
    //jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    //Sliding
    public GameObject normalHitbox;
    public GameObject slideHitbox;

    //misc
    public Transform firepoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        facingRight = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartSlide();
            ab.SetBool("isSliding", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            StopSlide();
            ab.SetBool("isSliding", false);
        }
    }

    private void FixedUpdate()
    {
        //Jumping
        float jump = Input.GetAxis("Jump");
        ab.SetFloat("verticalSpeed", Mathf.Abs(jump));
        if (grounded && jump > 0)
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length>0) grounded = true;
        else grounded = false;
        //Moving
        float move = Input.GetAxis("Horizontal");
        ab.SetFloat("Speed", Mathf.Abs(move));
        rb.velocity = new Vector3(move * speed, rb.velocity.y, 0);
        if (move > 0 && !facingRight)
        {
            Flip();
            firepoint.localRotation = Quaternion.identity;
        }
        else if (move < 0 && facingRight)
        {
            Flip();
            firepoint.localRotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
    void StartSlide()
    {
        normalHitbox.SetActive(false);
        slideHitbox.SetActive(true);
    }
    void StopSlide()
    {
        normalHitbox.SetActive(true);
        slideHitbox.SetActive(false);
    }
}
