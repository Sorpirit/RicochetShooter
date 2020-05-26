using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLoader : MonoBehaviour
{
    

    public void Load(int id)
    {
        SceneManager.LoadScene(id);
    }
}
[System.Serializable]
public enum SceneID
{
    Menu = 0,
    TestJoystick,
    TestTouchInput,
    TestTouchInputVertical
}