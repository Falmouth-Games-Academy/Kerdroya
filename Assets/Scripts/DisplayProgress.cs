using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayProgress : MonoBehaviour
{

    public AppProgression progress;
    public void Display()
    {
        int i = 0;
        foreach (bool level in AppProgression.levelCompleted)
        {
            gameObject.GetComponent<Text>().text += "Level " + i  + " Completed " + level + "\n";
            i++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AppProgression.levelCompleted[0] = true;
            progress.SaveGame();
        }
    }
}
