using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject StainedWindowPanel;

    //selectively render each window piece according to AppProgression data
    void Start()
    {
        //!!    ASSUMES APP PROGRESSION ORDER = WINDOW HIERARCHY ORDER  !!
        Transform[] children = StainedWindowPanel.GetComponentsInChildren<Transform>();
        for( int count = 0; count < AppProgression.levelCompleted.Length; count++)
        {
            //count+2 used to account for background image and parent's own transform
            children[count+2].gameObject.SetActive(AppProgression.levelCompleted[count]);
        }
    }
}
