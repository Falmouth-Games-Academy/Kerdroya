using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDetails : MonoBehaviour
{
    public GameObject selectedObject;
    public Text position;
    public Text rotation;
    public Text scale;

    void Update()
    {
        if (selectedObject != null)
        {
            position.text = "Pos: " + selectedObject.transform.position;
            rotation.text = "Rot: " + selectedObject.transform.rotation;
            scale.text = "Sca: " + selectedObject.transform.localScale;
        }
    }

    public void increaseScale()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.localScale = Vector3.Scale(selectedObject.transform.localScale, new Vector3(1.1f, 1.1f, 1.1f));
        }
    }

    public void decreaseScale()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.localScale = Vector3.Scale(selectedObject.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f));
        }
    }


}
