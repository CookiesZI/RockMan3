using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dxBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playercomponant))
        {
            playercomponant.TakeDamage(1);
        }
    }
}
