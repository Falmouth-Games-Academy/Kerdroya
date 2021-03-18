using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionReference : MonoBehaviour
{
    public MinigameProgressTracker miniGameProgressTracker;
    public RockTossScript rockTossScript;
    public bool success = false;


    private void OnTriggerExit(Collider colli)
    {
        if (colli.gameObject.layer == 11)
        {
            Debug.Log("Exit actualtarget"+colli.gameObject.name);
            success = true;
            rockTossScript.success = true;
            miniGameProgressTracker.points++;
        }
        if (colli.gameObject.layer != 11 && success == false)
        {
            rockTossScript.ResetPuzzle();
        }
    }
}