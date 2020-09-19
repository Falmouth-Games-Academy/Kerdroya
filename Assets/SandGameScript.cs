using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandGameScript : MonoBehaviour
{
    public TransitionHandler THandler;

    public GameObject targetObject;

    public GameObject indicator;

    public Transform targetStart;
    public Transform targetEnd;

    public float sinLength = 15f;
    public float sinOffset = 0f;

    public LayerMask hitLayers;
    public float detectionRadius = 1f;

    public MinigameProgressTracker MGPT;

    public int minigameState = 0;

    public GameObject arrow;

    // Update is called once per frame
    void Update()
    {
        if (THandler.sceneState == 3)
        {
            MainLoop();
        }
    }

    public void MainLoop()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            if (Vector3.Distance(indicator.transform.position, hit.point) < detectionRadius)
            {
                //indicator.transform.localPosition = Vector3.Lerp(indicator.transform.position, hit.point, 0.5f);

                indicator.transform.localPosition = Vector3.Lerp(
                        indicator.transform.localPosition,
                        GetClosestPointOnLineSegment(
                            targetStart.localPosition, 
                            targetEnd.localPosition, 
                            targetObject.transform.localPosition),
                        0.5f);
            }
        }
    }

    public void updateTrigger(int inValue)
    {
        if(minigameState == 1)
        {
            arrow.transform.localRotation = Quaternion.identity;
            if(inValue == 0)
            {
                THandler.sceneState = 4;
            }
        }
        else
        {
            minigameState = inValue;
        }
    }


    public Vector3 GetClosestPointOnLineSegment(Vector3 A, Vector3 B, Vector3 P)
    {
        Vector3 AP = P - A;       //Vector from A to P   
        Vector3 AB = B - A;       //Vector from A to B  

        float magnitudeAB = AB.magnitude * AB.magnitude;           //Magnitude of AB vector (it's length squared)     
        float ABAPproduct = Vector3.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
        float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  (0 to 1)
        
        if (distance < 0)         //Check if P projection is over vectorAB     
        {
            return A;

        }
        else if (distance > 1)
        {
            return B;
        }
        else
        {
            //generate sine wave
            var localOffset = new Vector3(Mathf.Sin(sinOffset + distance * sinLength * 6.28f), 0, 0);
            return A + localOffset +  AB * distance;
        }
    }
}
