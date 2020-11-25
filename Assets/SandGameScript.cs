using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject sandA, sandB, sandC = null;
    public GameObject goalA, goalB, goalC = null;

    public float WidthModifier = 1;
    private int HeldSandID = 1;

    public float distance;

    Scene scene;

    private void Start()
    {
         scene = SceneManager.GetActiveScene();
    }
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
            if (scene.name == "Cape Cornwall-11")
            {
                switch (HeldSandID)
                {
                    case 1: sandA.transform.position = indicator.transform.position; break;
                    case 2: sandB.transform.position = indicator.transform.position; break;
                    case 3: sandC.transform.position = indicator.transform.position; break;
                }
            }
        }
    }

    public void updateTrigger(int inValue)
    {
       
        if (scene.name == "Cape Cornwall-11")
        {
            if (inValue == 1)
            {
                switch (HeldSandID)
                {
                    case -1: break;
                    case 1:
                        sandA.transform.position = goalA.transform.position;
                        break;
                    case 2:
                        sandB.transform.position = goalB.transform.position;
                        break;
                    case 3:
                        sandC.transform.position = goalC.transform.position;
                        break;
                }
                if (HeldSandID != -1)
                {
                    MGPT.points++;
                    HeldSandID = -1;
                }
            }
            else
            {
                HeldSandID = MGPT.points;
            }
        }
        else
        {
            if (minigameState == 1)
            {
                arrow.transform.localRotation = Quaternion.identity;
                if (inValue == 0)
                {
                    THandler.sceneState = 4;
                }
            }
            else
            {
                minigameState = inValue;
            }
        }
    }


    public Vector3 GetClosestPointOnLineSegment(Vector3 A, Vector3 B, Vector3 P)
    {
        Vector3 AP = P - A;       //Vector from A to P   
        Vector3 AB = B - A;       //Vector from A to B  

        float magnitudeAB = AB.magnitude * AB.magnitude;           //Magnitude of AB vector (it's length squared)     
        float ABAPproduct = Vector3.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
        distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  (0 to 1)
        
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
            var localOffset = new Vector3(Mathf.Sin(sinOffset + distance * sinLength * 6.28f) * WidthModifier, 0, 0);
            return A + localOffset +  AB * distance;
        }
    }
}
