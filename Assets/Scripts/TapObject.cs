using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObject : MonoBehaviour
{
    public Material changeMaterial;
    public BallMaze maze;
    public Sigil sigil;
    public bool isMaze;
    public void activate()
    {
        gameObject.GetComponent<MeshRenderer>().material = changeMaterial;
        if (isMaze == true)
        {
            maze.Activate();
        }
        else
        {
            sigil.Activate();
        }
    }
}
