using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int maxReflectCount;
    [SerializeField] private AudioClip hitWall;
    [SerializeField] private AudioClip hitBonusWall;
    [SerializeField] private GameObject bulletExpierdEffect;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private Gradient hotBulletGradient;
    [SerializeField] private SpriteRenderer bulletSprite;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private TrailRenderer bulletTrail;

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
                Instantiate(bulletHole, transform.position, Quaternion.Euler(0,0,Random.Range(0,360)));
                source.pitch = 1f + 1.25f*reflectCount / maxReflectCount;
                source.clip = hitWall;
            }
            source.Play();
            
            reflectCount++;
            Color newBulletColor = hotBulletGradient.Evaluate((float) reflectCount / maxReflectCount);
            bulletSprite.color = newBulletColor;

            // Gradient newGrad = new Gradient();
            // GradientColorKey colorKey = new GradientColorKey {color = newBulletColor,time = 0f};
            // GradientColorKey colorKeyEnd = new GradientColorKey {color = Color.white,time = 1f};
            // newGrad.SetKeys(new[]{colorKey,colorKeyEnd},bulletTrail.colorGradient.alphaKeys);
            newBulletColor.r *= .25f;
            newBulletColor.g *= .25f;
            newBulletColor.b *= .25f;
            newBulletColor.a = .75f;
            bulletTrail.startColor = newBulletColor;
            newBulletColor.a = .01f;
            bulletTrail.endColor = newBulletColor;

            if (reflectCount >= maxReflectCount)
            {
                Instantiate(bulletExpierdEffect, transform.position, Quaternion.identity);
                Die();
            }
        }
        
        if (other.collider.CompareTag("Enemy"))
        {
            impulseSource.GenerateImpulse();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
}
