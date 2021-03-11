using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanelONOff : MonoBehaviour
{

    public GameObject debugPanel;
    public bool active = false;

    public void TogglePanel()
    {
        active = !active;
        debugPanel.SetActive(active);
    }
}
