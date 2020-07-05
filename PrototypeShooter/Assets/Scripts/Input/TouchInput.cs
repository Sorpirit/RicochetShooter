using UnityEngine;

[RequireComponent(typeof(IPlayerShooting))]
public class TouchInput : MonoBehaviour
{
    [SerializeField] private Vector2 screenDead;
    [SerializeField] private SpriteRenderer touchVisualiser;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private bool inverse;

    public bool IsInverted
    {
        set => inverse = value;
        get => inverse;
    }

    public bool UseButton;


    private IPlayerShooting shooting;
    private Camera cam;

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


        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 touchWorldPos = cam.ScreenToWorldPoint(touch.position);

            Vector2 lookDir = (touchWorldPos - (Vector2) transform.position).normalized;
            int inv = inverse ? -1 : 1;
            shooting.playerLookDir = lookDir * inv;
            touchVisualiser.transform.position = touchWorldPos;

            LeanTween.alpha(touchVisualiser.gameObject, 1, fadeOutTime);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (!UseButton)
                shooting.Shoot();
            LeanTween.alpha(touchVisualiser.gameObject, 0, fadeOutTime);
        }
    }

    public void Shoot()
    {
        if (UseButton)
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
        Gizmos.DrawCube(Vector2.zero, size * 2);
    }
}