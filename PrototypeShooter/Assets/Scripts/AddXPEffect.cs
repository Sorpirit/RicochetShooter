using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddXPEffect : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform targetPos;

    private void Start()
    {
        Invoke(nameof(ChengPos),.5f);
    }

    private void ChengPos()
    {
        Vector2 gotToPos = cam.ScreenToWorldPoint(targetPos.position);
        transform.position = gotToPos;
    }
}
