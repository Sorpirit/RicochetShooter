using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSpliter : MonoBehaviour
{
    [SerializeField] private Transform roomCollection;
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private int curentRoom;
    [SerializeField] private float switchDelaySeconds;
    [SerializeField] private PlayerShooting Shooting;
    [SerializeField] private ParticleSystem failGameEffect;
    [SerializeField] private GameObject failTextBackground;
    [SerializeField] private GameObject failText;
    [SerializeField] private float sceneMaxTimer;
    [SerializeField] private Slider timerUI;

    [SerializeField] private Transform centerPoint;

    [SerializeField] private AudioClip playerDead;
    [SerializeField] private AudioClip enemyDead;
    [SerializeField] private AudioSource soundSource;
    
    
    private RoomDirector[] rooms;
    private WaitForSeconds switchDealy;
    private bool isSwiching;
    private bool isEndingGame;
    private float timer;
    private bool autoDeath;

    private void Awake()
    {
        rooms = roomCollection.GetComponentsInChildren<RoomDirector>();
    }

    private void Start()
    {
        switchDealy = new WaitForSeconds(switchDelaySeconds);
        StartLevel(curentRoom);
        timer = sceneMaxTimer;
    }

    private void Update()
    {
        if (rooms[curentRoom].IsRoomDone && !isSwiching)
        {
            StartCoroutine(SwitchRoom());
            
        }

        if ((Shooting.CurrentBullets <= 0 || timer <= 0) && !isEndingGame && !isSwiching)
        {
            StartCoroutine(EndGame());
        }

        if (Shooting == null && !isEndingGame)
        {
            autoDeath = true;
            StartCoroutine(EndGame());
        }

        timer -= Time.deltaTime;
        timerUI.value = timer / sceneMaxTimer;
    }

    private IEnumerator SwitchRoom()
    {
        isSwiching = true;
        soundSource.clip = enemyDead;
        soundSource.Play();
        yield return switchDealy;
        StopLevel(curentRoom);
        curentRoom++;
        if (curentRoom >= rooms.Length)
            curentRoom = 0;
        StartLevel(curentRoom);
        timer = sceneMaxTimer;
        Shooting.CurrentBullets+=2;
        
        isSwiching = false;
    }

    private IEnumerator EndGame()
    {
        isEndingGame = true;
        failTextBackground.SetActive(true);
        if (!autoDeath)
        {
            Time.timeScale = .2f;
            Time.fixedDeltaTime *= Time.timeScale;
            yield return switchDealy;
        
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime;

            if (isSwiching || (Shooting != null && Shooting.CurrentBullets > 0))
            {
                failTextBackground.SetActive(false);
                isEndingGame = false;
                yield break;
            }
        }
        
        soundSource.clip = playerDead;
        soundSource.Play();

        failText.SetActive(true);
        failGameEffect.transform.position = centerPoint.transform.position;
        failGameEffect.Play();
        yield return switchDealy;
        isEndingGame = false;
        SceneManager.LoadScene(0);
    }

    private void StartLevel(int index)
    {
        if(index < 0 || index >= rooms.Length)
            return;
        
        rooms[index].StartAction();
        virtualCamera.Follow = rooms[index].Centroid;
        Shooting.transform.position = centerPoint.position;
    }

    private void StopLevel(int index)
    {
        if(index < 0 || index >= rooms.Length)
            return;
        Debug.Log("Stop level " + index);
        rooms[index].StopAction();
    }
}
