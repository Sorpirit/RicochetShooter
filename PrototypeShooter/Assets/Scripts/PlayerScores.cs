using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerScores : MonoBehaviour
    {
        public static PlayerScores instace;
        
        [SerializeField] private int scores;
        [SerializeField] private TMP_Text scoresText;

        private void Awake()
        {
            if (instace == null)
            {
                instace = this;
            }else if (instace != this)
            {
                Destroy(this);
                return;
            }
            
            scoresText.text = scores.ToString();
        }

        public void AddScores(int addPoints)
        {
            scores += addPoints;
            scoresText.text = scores.ToString();
        }
    }
}