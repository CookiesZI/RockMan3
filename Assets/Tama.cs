using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tama : MonoBehaviour
{
    Rigidbody rb;
    Animator ab;
    public Transform firepoint;
    public GameObject ball;
    public float speed = 3;
    public int currentball;
    public int maxBall = 3;

    public float lineOfSite;
    private Transform player;

    private float fireRate = 2f;
    private float nextFire = 0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        ab = GetComponent<Animator>();
        currentball = maxBall;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && Time.time > nextFire)
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        if(currentball > 0)
        {
            nextFire = Time.time + fireRate;
            GameObject Ball = Instantiate(ball, firepoint.position, ball.transform.rotation);
            ab.SetTrigger("isAttack");
            currentball--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
