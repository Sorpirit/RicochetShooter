using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashController : MonoBehaviour
{
    [SerializeField] private GameObject effectorPoint;
    [SerializeField] private float timeToDest;
    
    public GameObject EffectorPoint => effectorPoint;

    private void Start()
    {
        Destroy(gameObject,timeToDest);
    }
}
