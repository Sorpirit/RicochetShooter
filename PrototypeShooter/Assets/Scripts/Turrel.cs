using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Turrel : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float angel;
        [SerializeField] private float bulletSpeed;

        [SerializeField] private Animator animator;

        [SerializeField] private GameObject dieEffect;
        
        private float fireTimer;
        

        private void Update()
        {
            if(player == null)
                return;
            
            Vector2 dir = player.transform.position - firePoint.position;
            dir.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward,dir), 
                rotateSpeed * Time.deltaTime);
            if (fireTimer <= 0 && Vector2.Angle(transform.up,dir) <= angel)
            {
                Fire();
            }

            fireTimer -= Time.deltaTime;
        }

        private void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (bullet.TryGetComponent(out Rigidbody2D bRb))
            {
                bRb.velocity = transform.up * bulletSpeed;
            }
            fireTimer = fireRate;
            animator?.SetTrigger("Shoot");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out BulletController bullet))
            {
                
                
                Vector2 dir = player != null ? player.transform.position - firePoint.position : transform.up;
                dir.Normalize();
                transform.up = dir;
                Instantiate(dieEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    // kk
}