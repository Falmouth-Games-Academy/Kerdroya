using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeUI : MonoBehaviour
{
    public int levelIndex = 0;
    public bool desiredState = true;
    void Start()
    {
        if (AppProgression.levelCompleted[levelIndex] != desiredState) gameObject.SetActive(false);
    }

}
