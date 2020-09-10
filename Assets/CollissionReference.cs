using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionReference : MonoBehaviour
{
    public bool hitWall = false;

    void OnCollisionEnter(Collision colli)
    {
        if (colli.gameObject.layer == 11)
        {
            hitWall = true;
        }
    }

    private void OnCollisionExit(Collision colli)
    {
        if (colli.gameObject.layer == 11)
        {
            hitWall = false;
        }
    }
}