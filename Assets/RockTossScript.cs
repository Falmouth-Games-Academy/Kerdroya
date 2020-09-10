using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTossScript : MonoBehaviour
{

    //Ball prefab
    public GameObject rock;
    public Transform SpwanPoint;
    private Vector3 LastFramePos;
    private Vector3 EndPos;
    private Vector3 StartPos;
    public float MaxBallSpeed = 40;
    private float heldDownTime = 0f;
    public float MinSwipeDistance = 2f;
    public GameObject targetObject;
    public LayerMask hitLayers;
    private bool tossComplete, rockThrown, dragActive = false;
    private float rockScale = 1;
    private float gravity = 0;
    public GameObject GoalCollider;
    public MinigameProgressTracker MGPT;
    private float rockYlastFrame,rockYCurrentFrame = 0;
    //warning: lazy implementation
    //requires cursor to have moved at least (MinSwipeDistance)
    //then just compares current and last frame locations
    //then sets momentum to the difference of the two frames against time.

    private void Start()
    {
        LastFramePos = new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
    }

    void FixedUpdate()
    {   
        if (rock.GetComponent<CollissionReference>().hitWall){
            ResetPuzzle();
        }
        if (GoalCollider.GetComponent<GoalCollission>().hitGoal)
        {
            if (rockYCurrentFrame < rockYlastFrame) // rock is falling
            {
                MGPT.points++;
                ResetPuzzle();
            }
        }

        if (!rockThrown)
        {
            if (Input.GetMouseButton(0))
            { Dragging(); }
            if (Input.GetMouseButtonUp(0))
            { ReleaseAndFling(); }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            { ResetMinigame(); }
        }
        else
        {
            if (tossComplete)
            { ResetPuzzle(); }
            if (rockThrown)
            { RunRockPhysics(); }
        }
    }

    private void RunRockPhysics()
    {
        rockScale = rockScale * 0.99f;
        gravity += 0.001f;
        rock.transform.localPosition = rock.transform.localPosition - new Vector3(0, gravity, 0);
        rock.transform.localScale = Vector3.one * rockScale;
        rockYlastFrame = rockYCurrentFrame;
        rockYCurrentFrame = rock.transform.localPosition.y;
    }

    private void ResetPuzzle()
    {
        ResetMinigame();
        rockThrown = false;
        rockScale = 1;
        gravity = 0;
        rock.transform.localScale = Vector3.one;
        rock.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 180f));
        rock.transform.localPosition = SpwanPoint.transform.localPosition;
        rock.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        rock.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
    }

    void ReleaseAndFling()
    {
        if (true) {
            if (LastFramePos != new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity))
            {
                float swipedistance = Vector3.Magnitude(StartPos - EndPos);

                if (swipedistance > MinSwipeDistance)
                {
                    Vector3 dir = (EndPos - StartPos).normalized;
                    rock.GetComponent<Rigidbody>().AddForce(dir * 240f);
                    rockThrown = true;
                }
                else
                {
                    ResetMinigame();
                }
            }
        }
    }

    void Dragging()
    {
        if (dragActive)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            //hit game background
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
            {
                LastFramePos = EndPos;
                EndPos = hit.point;
                if (float.IsNegativeInfinity(LastFramePos.x))
                {
                    StartPos = hit.point;
                }
            }
            else
            {
                ResetMinigame();
            }
        }
        else dragActive = true;
    }

    void ResetMinigame()
    {
        LastFramePos = new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
        EndPos =  new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
        StartPos = new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);
    }
}