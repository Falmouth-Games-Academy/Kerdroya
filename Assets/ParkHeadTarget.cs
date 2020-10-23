using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkHeadTarget : MonoBehaviour
{
    public ParkHeadScript PHS;
    public int ID;
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            PHS.TargetClicked(ID);
        }
    }
}
