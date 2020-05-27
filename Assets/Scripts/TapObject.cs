using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObject : MonoBehaviour
{
    public Material changeMaterial;
    public BallMaze maze;
    public void activate()
    {
        gameObject.GetComponent<MeshRenderer>().material = changeMaterial;
        maze.Activate();

    }
}
