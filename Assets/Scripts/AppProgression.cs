using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppProgression : MonoBehaviour
{
    private bool created = false;
    public bool[] levelCompleted = new bool[12];

    // Prevent this object from being destroyed between scenes loading.
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateLevelCompleted(int levelIndex)
    {
        levelCompleted[levelIndex] = true;
    }

}
