using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishboneBehaviour : MonoBehaviour
{

    public Transform startLocation;
    public Transform endLocation;
    public ShowRotation showRotation;
    public MinigameProgressTracker minigameProgressTracker;

    public bool top = false;
    public bool bottom = false;

    void CheckProgress()
    {
        if (top && bottom)
        {
            minigameProgressTracker.points += 1;
            top = false;
            bottom = false;
        }

        if (showRotation.tiltPercentage > 0.9)
        {
            top = true;
        }

        if (showRotation.tiltPercentage < 0.1)
        {
            bottom = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckProgress();

        transform.position = Vector3.Lerp(startLocation.position, endLocation.position, showRotation.tiltPercentage);
    }
}
