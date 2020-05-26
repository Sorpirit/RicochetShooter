using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarHandler : MonoBehaviour
{
    [SerializeField] private Slider valSlider;
    [SerializeField] private Slider tempValSlider;
    [SerializeField] private GameObject edjeBarXP;

    [SerializeField] private float delayTimer;
    [SerializeField] private float animateTime;
    
    public float MaxVal
    {
        set
        {
            valSlider.maxValue = value;
            tempValSlider.maxValue = value;
        }
    }

    public float Val
    {
        set
        {
            tempLevel = value;
            tempValSlider.value = tempLevel;
            if (!isAnimating)
                StartCoroutine(Animate());
        }
    }

    public GameObject EdjeBarXp => edjeBarXP;

    private float maxVal;
    private float val;
    private float tempLevel;
    private bool isAnimating;
    
    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(delayTimer);
        float animTimer = 0;
        isAnimating = true;
        while (tempLevel > val && isAnimating)
        {
            animTimer += Time.deltaTime;
            val = Mathf.Lerp(val, tempLevel, animTimer / animateTime);
            if (tempLevel - val < .1f)
                val = tempLevel;
            valSlider.value = val;
            
            yield return null;
        }

        isAnimating = false;
    }

}
