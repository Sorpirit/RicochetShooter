using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BonusWall : MonoBehaviour
{
    [SerializeField] private int scores;
    [SerializeField] private FloatinPopup addPointsText;
    [SerializeField] private bool useBulletReflectCount;
    [SerializeField] private float maxReflectionCount;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.TryGetComponent(out BulletController bullet))
        {
            float scoresMultiplier = 0;
            
            if(useBulletReflectCount)
                scoresMultiplier = Mathf.Clamp(bullet.ReflectCount, 0, maxReflectionCount) / maxReflectionCount;
            
            int addScores = (int) (scores * (1 + scoresMultiplier));
            
            GameObject popup = Instantiate(addPointsText.gameObject, other.contacts[0].point, Quaternion.identity);
            FloatinPopup popupContent = popup.GetComponent<FloatinPopup>();
            popupContent.SetText("+" + addScores + "xp");
            PlayerScores.instace?.AddScores(addScores);
        }
        
    }
}