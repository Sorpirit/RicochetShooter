using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class FloatinPopup : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private Vector2 flyDir;
    [SerializeField] private float liveTime;
    [SerializeField] private float growTimeProcentage;
    [SerializeField] private float growTo;
    [SerializeField] private float shrinkTo;

    private TMP_Text _label;
    private float liveTimer;
    private bool isGrowing = true;
    private Vector2 initialScale;
    private Vector2 targetScale;
    
    private void Awake()
    {
        _label = GetComponent<TMP_Text>();
        initialScale = transform.localScale;
        targetScale = initialScale * growTo;
    }

    private void Update()
    {
        liveTimer += Time.deltaTime;

        if (isGrowing)
        {
            transform.localScale = Vector2.Lerp(initialScale,targetScale,liveTimer/(growTimeProcentage * liveTime));

            if (liveTimer >= growTimeProcentage * liveTime)
            {
                isGrowing = false;
                targetScale = initialScale * shrinkTo;
                initialScale = transform.localScale;
            }
        }
        else
        {
            transform.localScale = Vector2.Lerp(initialScale,targetScale,(liveTimer - growTimeProcentage * liveTime)/liveTime);
            
        }

        transform.position +=(Vector3) flyDir * (flySpeed * Time.deltaTime);

        if (liveTimer >= liveTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        _label.text = text;
    }
}
