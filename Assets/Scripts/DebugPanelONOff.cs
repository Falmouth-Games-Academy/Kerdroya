using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanelONOff : MonoBehaviour
{

    public GameObject debugPanel;
    public int taps;

    public void buttonTapped()
    {
        taps++;
        if (taps>10)
        {
            debugPanel.SetActive(true);
            taps = 0;
        }
    }



}
