using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMinigame : MonoBehaviour
{
    public GameObject indicator;
    public Transform CircleCenter;
    public float radius = 3f;
    public float minDragDistance = 1f;
    public ElementDragger dragger;
    public TransitionHandler THandler;
    public bool[] activegate = new bool[3];
    int startID = 0;

    private void Update()
    {
        if (THandler.sceneState == 3) // prevents indicator from getting trapped, if Transition handler scene system is changed, this value will need updating
        {
            //if close to outer circle
            if (Vector3.Distance(indicator.transform.position, dragger.targetObject.transform.position) < minDragDistance)
            {
                //move outer circle about inner circle according to mouse position (dragger object)
                indicator.transform.position = GetPointOnRadius(dragger.targetObject.transform, CircleCenter, radius);
            }
        }
    }

    public void updateTrigger(int triggerID)
    {
        if (!activegate[0] && !activegate[1] && !activegate[2])
        {
            startID = triggerID;
        }
        activegate[triggerID] = true;

        if (activegate[0] && activegate[1] && activegate[2] && startID == triggerID) {
            //TODO: begin transition phase

            //complete game, move scene handler phase
            THandler.sceneState = 4;
        }
    }


    public Vector3 GetPointOnRadius( Transform target, Transform centre, float radius)
    {
        Vector3 point= centre.InverseTransformPoint(target.position);
        return centre.TransformPoint(point.normalized * radius);
    }
}
