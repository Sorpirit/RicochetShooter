using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int maxReflectCount;
    [SerializeField] private AudioClip hitWall;
    [SerializeField] private AudioClip hitBonusWall;
    [SerializeField] private GameObject bulletExpierdEffect;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private AudioSource source;
    private int reflectCount;

    private void Start()
    {
        reflectCount = maxReflectCount;
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            if (other.gameObject.TryGetComponent(out BonusWall wall))
            {
                source.clip = hitBonusWall;
                impulseSource.GenerateImpulse();
                reflectCount += 2;
            }
            else
            {
                
                source.clip = hitWall;
                //source.PlayOneShot(hitWall);
            }
            source.Play();
            
            reflectCount--;

            if (reflectCount <= 0)
            {
                Instantiate(bulletExpierdEffect, transform.position, Quaternion.identity);
                Die();
            }
        }
        
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            impulseSource.GenerateImpulse();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
}
