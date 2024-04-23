using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltonNutton : MonoBehaviour
{
    Animator ab;

    public float speed;
    public float lineOfSite;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ab = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playercomponant))
        {
            playercomponant.TakeDamage(1);
            ab.SetTrigger("bAttack");
        }
    }
}
