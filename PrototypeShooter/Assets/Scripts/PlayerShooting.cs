using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform fierPoint;
    [SerializeField] private float fierRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int maxBulets;
    [SerializeField] private BulletCountUI bulletCountUi;

    [SerializeField] private LineRenderer projectTragectory;
    
    [SerializeField] private AudioClip buletShootSound;
    [SerializeField] private AudioClip buletLastShootSound;
    [SerializeField] private AudioSource soundsAudio;

    [SerializeField] private Animator playerAnimator;
    
    
    private Camera cam;
    private float fierTimer;
    private int currentBullets;

    public int CurrentBullets
    {
        get => currentBullets;
        set
        {
            currentBullets = value;
            currentBullets = Mathf.Clamp(currentBullets, 0, maxBulets); 
            bulletCountUi?.SetBulletsActiveAmount(currentBullets);
        }
    }

    private void Start()
    {
        currentBullets = maxBulets;
        bulletCountUi?.SetBulletsActiveAmount(currentBullets);
        
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && fierTimer >= fierRate && currentBullets > 0)
        {
            Fier();
        }
        Rotate();
        CalculateTrajectory();
        fierTimer += Time.deltaTime;
    }

    private void Rotate()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        transform.up = (mousePos - (Vector2) transform.position).normalized;
    }

    private void Fier()
    {
        GameObject bulletTile = Instantiate(bullet,fierPoint.position,fierPoint.rotation);

        if (bulletTile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = fierPoint.up * bulletSpeed;
        }

        soundsAudio.clip =currentBullets == 1 ? buletLastShootSound : buletShootSound;
        soundsAudio.Play();
        
        currentBullets--;
        bulletCountUi?.SetBulletsActiveAmount(currentBullets);
        
        playerAnimator?.SetTrigger("Shoot");
    }

    private void CalculateTrajectory()
    {
        RaycastHit2D firstHit = Physics2D.Raycast(fierPoint.position, fierPoint.up);
        Vector2 reflectDir = Vector2.Reflect(fierPoint.up,firstHit.normal);
        RaycastHit2D secondHit = Physics2D.Raycast(firstHit.point, reflectDir);
        Debug.DrawRay(firstHit.point,reflectDir,Color.red);
        Debug.DrawRay(firstHit.point,fierPoint.up,Color.green);
        Debug.DrawRay(firstHit.point,firstHit.normal,Color.black);
        Debug.DrawRay(secondHit.point,secondHit.normal,Color.cyan);
        Vector2 secodPoint = secondHit.collider != firstHit.collider? secondHit.point : firstHit.point + 4 * reflectDir;
        projectTragectory.SetPosition(0,fierPoint.position);
        projectTragectory.SetPosition(1,firstHit.point);
        projectTragectory.SetPosition(2,secodPoint);
    }
}
