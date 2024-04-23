using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemybComponent))
        {
            enemybComponent.TakeDamageE(1);
            Destroy(gameObject);
        }
    }
}
