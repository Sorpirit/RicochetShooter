using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    [SerializeField] private GameObject bulletExplosion;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            impulseSource.GenerateImpulse((other.transform.position - transform.position).normalized);
            Instantiate(bulletExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        if (!other.TryGetComponent(out BulletController controller))
        {
            Destroy(gameObject);
        }
    }
}
