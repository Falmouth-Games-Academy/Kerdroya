using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkHeadScript : MonoBehaviour
{
    public MinigameProgressTracker MGPT;



    public void TargetClicked(int targetID)
    {
        MGPT.points++;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
