using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilWaypoint : MonoBehaviour
{
    private bool waypointActivated = false;

    public int numberInOrder;

    public Sigil parent;

    public Material activeMaterial;
    public Material inactiveMaterial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FingerTracker")
        {
            if (parent.progressNumber == numberInOrder - 1)
            {
                gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
                waypointActivated = true;
            }
        }
    }


    public void DisableWaypoint()
    {
        gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
        waypointActivated = false;
    }

}
