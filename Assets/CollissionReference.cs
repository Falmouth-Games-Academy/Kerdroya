using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionReference : MonoBehaviour
{
    public bool hitWall = false;

    void OnTriggerEnter(Collider colli)
    {
        Debug.Log("Enter" + colli.gameObject.name);
        if (colli.gameObject.layer == 11)
        {
            Debug.Log("Enter" + colli.gameObject.name);
            hitWall = true;
        }
    }

    private void OnTriggerExit(Collider colli)
    {
        Debug.Log("Exit" + colli.gameObject.name);
        if (colli.gameObject.layer == 11)
        {
            Debug.Log("Exit"+colli.gameObject.name);
            hitWall = false;
        }
    }
}