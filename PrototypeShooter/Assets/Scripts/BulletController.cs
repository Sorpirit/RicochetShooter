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
    [SerializeField] private Gradient hotBulletGradient;
    [SerializeField] private SpriteRenderer bulletSprite;

    public int ReflectCount => reflectCount;

    private AudioSource source;
    private int reflectCount;

    private void Start()
    {
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
                reflectCount ++;
            }
            else
            {
                source.clip = hitWall;
            }
            source.Play();
            
            reflectCount++;
            bulletSprite.color = hotBulletGradient.Evaluate((float)reflectCount/maxReflectCount);

            if (reflectCount >= maxReflectCount)
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
