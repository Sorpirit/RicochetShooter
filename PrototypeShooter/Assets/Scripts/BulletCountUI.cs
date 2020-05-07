using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletCountUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> bulets;

        private int lastBulletAmount;

        public void SetBulletsActiveAmount(int bulletAmount)
        {
            if(lastBulletAmount == bulletAmount && (bulletAmount < 0 || bulletAmount > bulets.Count))
                return;
            
            if (lastBulletAmount > bulletAmount)
            {
                int deActiveIterator = lastBulletAmount;
                while (deActiveIterator > bulletAmount)
                {
                    bulets[deActiveIterator - 1].SetActive(false);
                    deActiveIterator--;
                }
            }
            else
            {
                int activeIterator = lastBulletAmount;
                while (activeIterator < bulletAmount)
                {
                    bulets[activeIterator].SetActive(true);
                    activeIterator++;
                }
            }
            lastBulletAmount = bulletAmount;
            
        }
    }
}