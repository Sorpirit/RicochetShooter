using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTouch : MonoBehaviour
{
    [SerializeField] private TouchInput touchInput;
    
    public void InverseInput(bool inv)
    {
        touchInput.IsInverted = inv;
    }

    public void UseButton(bool isBtn)
    {
        touchInput.UseButton = isBtn;
    }
}
