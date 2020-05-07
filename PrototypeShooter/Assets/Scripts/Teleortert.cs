using UnityEngine;

namespace DefaultNamespace
{
    public class Teleortert : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject dieEffect;
        
        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.TryGetComponent(out BulletController bullet))
            {
                if(player != null)
                    player.transform.position = transform.position;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}