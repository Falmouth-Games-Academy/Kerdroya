using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableWall : MonoBehaviour
{
    public int objectID;
    public MinigameProgressTracker MGPT;
    public GameObject fingerTracker;
    public GameObject targetLocation;

    public bool dragActive = false;
    public bool atTarget = false;

    public Vector3 startPosition;

    private bool foundGoal = false;
    private bool onTracker = false;

    private void Start()
    {
        startPosition = this.transform.localPosition;
    }

    private void Update()
    {
        if (!foundGoal)
        {
            if (Input.GetMouseButtonUp(0))
            {
                dragActive = false;
                if (atTarget) {
                    foundGoal = true;
                    MGPT.points++;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (onTracker)
                {
                    dragActive = true;
                }
            }

            if (dragActive)
            {
                transform.position = fingerTracker.transform.position;
            }
            else
            {
                this.transform.localPosition = startPosition;
            }
        }
        else
        {
            this.transform.position = targetLocation.gameObject.transform.position;
        }
    }

    //test if at correct location
    void OnTriggerEnter(Collider coll)
    {
        if ((coll.gameObject.GetComponent("WallGoal") as WallGoal) != null)
        {
            WallGoal WG = coll.gameObject.GetComponent("WallGoal") as WallGoal;
            if (WG.wallID == objectID) // correctly aligned piece
            {
                atTarget = true;
            }
        }
        else if (coll.gameObject.name == "FingerTracker") 
        {
            onTracker = true;
        }

    }

    //if exiting correct location
    void OnTriggerExit(Collider coll)
    {
        if ((coll.gameObject.GetComponent("WallGoal") as WallGoal) != null)
        {
            WallGoal WG = coll.gameObject.GetComponent("WallGoal") as WallGoal;
            if (WG.wallID == objectID)
            {
                atTarget = false;
            }
        }
        else if (coll.gameObject.name == "FingerTracker")
        {
            onTracker = false;
        }
    }
}
