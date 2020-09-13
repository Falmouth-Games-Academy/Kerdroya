using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollissionTrigger : MonoBehaviour
{
    public int ID;
    public CircleMinigame CMG;

    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.layer == 11)
        {
            CMG.updateTrigger(ID);
        }
    }

}
