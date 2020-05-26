using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(IPlayerShooting))]
public class JoystickInput : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    private IPlayerShooting playerShooting;

    private void Awake()
    {
        playerShooting = GetComponent<IPlayerShooting>();
    }

    private void Update()
    {
        playerShooting.playerLookDir = joystick.Direction;
    }
}
