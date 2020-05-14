﻿using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private GameObject enemyExploded;
        [SerializeField] private GameObject addXPEffect;

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.TryGetComponent(out BulletController bullet))
            {
                Explode(other.contacts[0].point);
            }
        
        }
        
        public void Explode(Vector2 pos)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(enemyExploded, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Instantiate(addXPEffect, transform.position, Quaternion.identity);
            if (explosion.TryGetComponent(out SmashController smashController))
            {
                smashController.EffectorPoint.transform.position = pos;
            }
        }
    }
}