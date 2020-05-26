using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerScores : MonoBehaviour
    {
        public static PlayerScores instace;
        
        [SerializeField] private int scores;
        [SerializeField] private TMP_Text scoresText;
        [SerializeField] private XPBarHandler xpBarHandler;
        [SerializeField] private int maxScore;
        [SerializeField] private ParticleSystem pluseXp;

        private Camera cam;
        
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

            xpBarHandler.MaxVal = maxScore;
            scoresText.text = scores.ToString();
            cam = Camera.main;
        }

        public void AddScores(int addPoints)
        {
            scores += addPoints;
            xpBarHandler.Val = scores;
            scoresText.text = scores.ToString();
        }

        public void PlayAddXpEffect(Vector2 pos)
        {
            StartCoroutine(AnimatedXp(pos));
        }
        
        private IEnumerator AnimatedXp(Vector2 pos)
        {
            GameObject xp = Instantiate(pluseXp.gameObject, pos, Quaternion.identity);
            ParticleSystem xpParticals = xp.GetComponent<ParticleSystem>();
            Vector2 xpPos = cam.ScreenToWorldPoint(xpBarHandler.EdjeBarXp.transform.position);
            AnimationCurve particalsCurve = xpParticals.velocityOverLifetime.radial.curve;
            float time = xpParticals.main.duration - particalsCurve[1].time;
            float dist = (pos - xpPos).magnitude;
            float speed = -dist / time;
            Keyframe keyframe = new Keyframe();
            keyframe.time = particalsCurve[2].time;
            keyframe.value = speed;
            xpParticals.velocityOverLifetime.radial.curve.MoveKey(1, keyframe);
            //Debug.Log();
            yield return new WaitForSeconds(.1f);
            xp.transform.position = xpPos;
        }
    }
}