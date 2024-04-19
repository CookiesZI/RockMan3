using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pickelman : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    bool isfacingRight;

    Rigidbody rb;
    Animator ab;

    public float arrivalThreshold = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        targetPoint = 0;
        isfacingRight = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, patrolPoints[targetPoint].position);

        if (distance <= arrivalThreshold)
        {
            increaseTargetInt(); 
        }
        MoveTowardsPatrolPoint();
    }

    void MoveTowardsPatrolPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if(targetPoint >= patrolPoints.Length)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playercomponant))
        {
            ab.SetTrigger("isAttacking");
            playercomponant.TakeDamage(1);
        }
    }
}
