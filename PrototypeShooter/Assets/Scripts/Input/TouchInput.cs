using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(IPlayerShooting))]
public class TouchInput : MonoBehaviour
{
    [SerializeField] private Vector2 screenDead;
    [SerializeField] private SpriteRenderer touchVisualiser;
    [SerializeField] private float fadeOutTime;
    
    private IPlayerShooting shooting;
    private Camera cam;

    private void Awake()
    {
        shooting = GetComponent<IPlayerShooting>();
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.touchCount == 0)
            return;
        
        Touch touch = Input.touches[0];
        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 touchWorldPos = cam.ScreenToWorldPoint(touch.position);
            touchVisualiser.transform.position = touchWorldPos;
            Vector2 lookDir = (touchWorldPos - (Vector2) transform.position).normalized;
            shooting.playerLookDir = lookDir;
            LeanTween.alpha(touchVisualiser.gameObject, 1, fadeOutTime);
            
        }else if (touch.phase == TouchPhase.Ended)
        {
            shooting.Shoot();
            LeanTween.alpha(touchVisualiser.gameObject, 0, fadeOutTime);
        }
    }

}