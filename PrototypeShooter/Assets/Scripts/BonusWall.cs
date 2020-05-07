using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BonusWall : MonoBehaviour
{
    [SerializeField] private int scores;
    [SerializeField] private FloatinPopup addPointsText;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject popup = Instantiate(addPointsText.gameObject, other.contacts[0].point, Quaternion.identity);
        FloatinPopup popupContent = popup.GetComponent<FloatinPopup>();
        popupContent.SetText("+" + scores);
        PlayerScores.instace.AddScores(scores);
    }
}
