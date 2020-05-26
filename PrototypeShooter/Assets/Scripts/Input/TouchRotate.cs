using System;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private SpriteRenderer touchVisualiser;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float rotationAcuracy;
    [SerializeField] private float minTapTime;
    [SerializeField] private float maxTapTime;
    [SerializeField] private float maxMoveDelta;
    
    private IPlayerShooting shooting;
    private Camera cam;
    private bool isHiden = true;
    
    private bool isTaping;
    private Vector2 tapPos;
    private float deltaMove;
    private float tapTimer;
    
    private void Awake()
    {
        shooting = GetComponent<IPlayerShooting>();
        cam = Camera.main;
    }

    private void Update()
    {
        
        
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.touches[0];
        
        Vector2 touchWorldPos = cam.ScreenToWorldPoint(touch.position);
        touchVisualiser.transform.position = touchWorldPos;
        switch (touch.phase)
        {
            case TouchPhase.Began:
                isTaping = true;
                tapPos = touch.position;
                tapTimer = 0;
                deltaMove = 0;
                break;
            case TouchPhase.Moved:
            {
                if (isTaping)
                {
                    deltaMove += touch.deltaPosition.magnitude;
                    if (deltaMove >= maxMoveDelta)
                    {
                        isTaping = false;
                        Debug.Log("Brocken2");
                    }
                    else 
                        break;
                }


                Vector2 toucgDir = touch.deltaPosition.normalized;
                float axsis = Mathf.Abs(toucgDir.x) > Mathf.Abs(toucgDir.y) ? toucgDir.x : toucgDir.y;
            
                float inputDir = 1;

                if (Mathf.Abs(toucgDir.x) > Mathf.Abs(toucgDir.y))
                {
                    if (touchWorldPos.y > shooting.body.position.y)
                    {
                        inputDir = -1;
                    }
                    else
                    {
                        inputDir = 1;
                    }
                }
                else
                {
                    if (touchWorldPos.x > shooting.body.position.x)
                    {
                        inputDir = 1;
                    }
                    else
                    {
                        inputDir = -1;
                    }
                }
                if (axsis > 0)
                {
                    inputDir *= 1;
                }
                else if (axsis < 0)
                {
                    inputDir *= -1;
                }

                float speedMultiplayer = touch.deltaPosition.magnitude / rotationAcuracy;
                float rotatAngel = rotateSpeed * inputDir * speedMultiplayer * Time.deltaTime;
                Vector2 lookDir = shooting.playerLookDir;
                Debug.Log(touch.deltaPosition.magnitude);
                Vector3 rotatedVector = Quaternion.AngleAxis(rotatAngel, Vector3.forward) * lookDir;
                Debug.DrawRay(transform.position, lookDir, Color.red);
                Debug.DrawRay(transform.position, rotatedVector, Color.green);
                shooting.playerLookDir = rotatedVector.normalized;
                if (isHiden)
                    LeanTween.alpha(touchVisualiser.gameObject, 1, fadeOutTime);
                isHiden = false;

                break;
            }
            case TouchPhase.Ended:
            {
                if (isTaping && tapTimer >= minTapTime)
                {
                    shooting.Shoot();
                }
                else
                {
                    Debug.Log("Brocken3");
                }
                isTaping = false;
                tapTimer = 0;
                deltaMove = 0;
                
                if (!isHiden)
                    LeanTween.alpha(touchVisualiser.gameObject, 0, fadeOutTime);
                isHiden = true;
                break;
            }
        }
        if(isTaping)
            tapTimer += Time.deltaTime;

        if (tapTimer > maxTapTime)
        {
            isTaping = false;
            tapTimer = 0;
            Debug.Log("Brocken1");
        }
    }
}