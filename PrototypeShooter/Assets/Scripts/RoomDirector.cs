using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class RoomDirector : MonoBehaviour
    {
        [SerializeField] private Transform centroid;
        [SerializeField] private GameObject contrent;
        [SerializeField] private GameObject enemyPrefb;
        [SerializeField] private Transform enemySpawnsParent;

        private Transform[] enemySpawnPoints;
        
        public Transform Centroid
        {
            get => centroid;
        }
        public bool IsRoomDone => isRoomDone;

        private bool isRoomDone;
        private GameObject enemy;

        private void Awake()
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform t in enemySpawnsParent)
            {
                children.Add(t);
            }
            enemySpawnPoints = children.ToArray();
        }

        private void Update()
        {
            if (!isRoomDone && enemy == null)
            {
                isRoomDone = true;
            }
        }


        public void StartAction()
        {
            contrent.SetActive(true);
            SpanwEnemy();
        }

        public void StopAction()
        {
            Debug.Log("!!aStop level " + gameObject.name);
            isRoomDone = true;
            contrent.SetActive(false);
        }

        private void SpanwEnemy()
        {
            int randIndex = Random.Range(0, enemySpawnPoints.Length);
            isRoomDone = false;
            enemy = Instantiate(enemyPrefb, enemySpawnPoints[randIndex].position, enemySpawnPoints[randIndex].rotation);
        }
    }
}