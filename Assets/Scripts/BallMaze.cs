using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaze : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ballRef;
    public GameObject halfwayPoint;
    public GameObject endPoint;

    private bool active = false;
    private Quaternion gyroData;
    private float progress;
    public bool ending;
    public bool completed;
    private Vector3 ballStartingPoint;

    private void Start()
    {
        ballStartingPoint = ballRef.transform.position;
    }
    public void SetGyroData(Quaternion GyroData)
    {
        gyroData = new Quaternion(GyroData.x, GyroData.y, 0.0f, GyroData.z);
    }

    public bool isActive()
    {
        return active;
    }

    public void Activate()
    {
        active = true;
    }

    void Completed()
    {
        //End scene and load next one of perform some kind of action.
        print("Victory Achieved");
    }

    void ResetToStart()
    {
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        ballRef.transform.position = ballStartingPoint;
        ballRef.GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.rotation = gyroData;
            if (!ending) 
            { 
                progress = Vector3.Distance(ballRef.transform.position, halfwayPoint.transform.position); 
                if (progress < 1 && !ending)
                {
                    ending = true;
                }
            }
            else 
            { 
                progress = Vector3.Distance(ballRef.transform.position, endPoint.transform.position);
                if (progress < 1 && ending)
                {
                    completed = true;
                }
            }
            /* For debugging purposes to test mazes on PC */            
            //if (Input.GetKey(KeyCode.RightArrow))
            //    {
            //        transform.Rotate(+0.5f,0.0f,0.0f);
            //    }

            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    transform.Rotate(-0.5f, 0.0f, 0.0f);
            //}

        }
    }
}
