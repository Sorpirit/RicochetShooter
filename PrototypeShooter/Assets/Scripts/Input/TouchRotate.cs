using System;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private SpriteRenderer touchVisualiser;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float rotationAcuracy;
    [SerializeField] private Vector2 screenDead;

    private IPlayerShooting shooting;
    private Camera cam;
    private bool isHiden = true;

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

        Vector2 touchPoint = cam.ScreenToViewportPoint(touch.position);
        bool ignoreTouch = (touchPoint.x < screenDead.x || touchPoint.x > (1 - screenDead.x)) ||
                           (touchPoint.y < screenDead.y || touchPoint.y > (1 - screenDead.y));
        if (ignoreTouch)
            return;
        
        Vector2 touchWorldPos = cam.ScreenToWorldPoint(touch.position);
        touchVisualiser.transform.position = touchWorldPos;
        switch (touch.phase)
        {
            case TouchPhase.Moved:
            {
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
                if (!isHiden)
                    LeanTween.alpha(touchVisualiser.gameObject, 0, fadeOutTime);
                isHiden = true;
                break;
            }
        }
    }

    public void Shoot()
    {
        shooting.Shoot();
    }
    
    private void OnDrawGizmosSelected()
    {
        if (cam == null)
            cam = Camera.main;
        //Vector2 touchPoint = cam.ScreenToViewportPoint(touch.position);
        Color col = new Color(.7f, .2f, .05f, .35f);
        Gizmos.color = col;
        Vector2 maxViewPoint = Vector2.one - screenDead;
        Vector2 size = cam.ViewportToWorldPoint(maxViewPoint);
        Gizmos.DrawCube((Vector2) cam.transform.position, size * 2);
    }
}