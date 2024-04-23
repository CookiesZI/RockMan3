using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Animator ab;

    //Shooting
    public Transform shootPoint;
    public GameObject bullet;
    public float speed = 5f;

    void Start()
    {
        ab = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShootBullet();
            ab.SetTrigger("pAttack");
        }
    }

    void ShootBullet()
    {
        GameObject Bullet = Instantiate(bullet, shootPoint.position, bullet.transform.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();

        rb.AddForce(shootPoint.forward * speed, ForceMode.Impulse);
    }

}
