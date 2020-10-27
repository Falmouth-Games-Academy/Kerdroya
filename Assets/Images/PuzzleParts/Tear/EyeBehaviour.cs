using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{

    public Transform startLocation;
    public Transform endLocation;
    public SimpleDistanceMeasure simpleDistanceMeasure;
    public MinigameProgressTracker minigameProgressTracker;
    float progress = 0;
    public float sin;
    
    void SineWobble()
    {
        sin = Mathf.Sin(Time.time);
        progress = simpleDistanceMeasure.asPercentage;
        progress += (sin / 7);
        progress = Mathf.Clamp(progress, 0, 1);
       
    }


    // Update is called once per frame
    void Update()
    {
        SineWobble();

        transform.position = Vector3.Lerp(startLocation.position, endLocation.position, progress);
    }
}
