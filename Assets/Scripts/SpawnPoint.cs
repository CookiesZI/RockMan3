using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemySpawn;
    public float lineOfSite;
    private Transform player;
    private bool hasSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && !hasSpawned)
        {
            Spawn();
            hasSpawned = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    void Spawn()
    {
        Instantiate(enemySpawn, transform.position, enemySpawn.transform.rotation);
    }
}
