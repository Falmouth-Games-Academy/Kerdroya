using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkHeadScript : MonoBehaviour
{
    public GameObject[] targets;
    private int nextTarget = 0;
    public MinigameProgressTracker MGPT;
    public GameObject DragMarker;
    public Material replaceMentMaterial;
    public GameObject water;
    public GameObject waterGraphic;

    public void Start()
    {
        water.transform.position = targets[0].transform.position;
        DragMarker.transform.position = targets[0].transform.position;
    }

    public void Update()
    {
        water.transform.position = Vector3.Lerp(water.transform.position, DragMarker.transform.position, 0.05f);
        waterGraphic.transform.localPosition = 
            new Vector3(Mathf.Cos(Time.time)*0.1f, 
                       (Mathf.Cos(Time.time*0.5f)*0.03f)-0.5f,
                        0);
    }


    public void TargetClicked(int targetID)
    {
        if (nextTarget == targetID)
        {
            
            nextTarget++;
            MGPT.points++;
            //targets[targetID].GetComponent<Renderer>().material = replaceMentMaterial;
            targets[targetID].GetComponent<SpriteRenderer>().enabled = true;
            if (targetID < targets.Length-1)
            {
                DragMarker.transform.position = targets[targetID + 1].transform.position;
            }
        }
    }
}
