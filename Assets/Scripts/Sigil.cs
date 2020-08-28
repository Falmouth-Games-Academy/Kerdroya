using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sigil : MonoBehaviour

{
    // Start is called before the first frame update
    public GameObject[] waypoints;

    public GameObject fingerTracker;

    public int progressNumber = 0;

    private bool active = false;
    public bool completed;

    private void Start()
    {
        for (int i = 0;  i < waypoints.Length; i++)
        {
            waypoints[i].GetComponent<SigilWaypoint>().numberInOrder = i + 1;
            waypoints[i].GetComponent<SigilWaypoint>().parent = this;
        }    
        Activate();

    }
    public void Activate()
   {
        active = true;
   }

    // STUFF HERE

    void Completed()
    {
        //End scene and load next one of perform some kind of action.
        print("Victory Achieved");
    }

    void ResetToStart()
    {
        // Reset all components to beginning.
        foreach(GameObject point in waypoints)
        {
            point.GetComponent<SigilWaypoint>().DisableWaypoint();
        }
        progressNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (progressNumber == waypoints.Length)
            {
                AppProgression.levelCompleted[SceneManager.GetActiveScene().buildIndex - 1] = true;
            }
            if (Input.touchCount > 0)
            {
                for (var i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Moved)
                    {
                        Touch touch = Input.GetTouch(i);
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        if (Physics.Raycast(ray))
                        {
                            fingerTracker.transform.position = ray.GetPoint(10);
                        }
                    }
                }
            }
        }
    }

}
