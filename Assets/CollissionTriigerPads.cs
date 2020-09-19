using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionTriigerPads : MonoBehaviour
{
    public int ID;
    public SandGameScript SGS;

    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.layer == 11)
        {
            SGS.updateTrigger(ID);
        }
    }
}
