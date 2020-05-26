using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchActivated : MonoBehaviour
{


    private RaycastHit hit;
    public LayerMask castLayer;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        hit.collider.GetComponent<TapObject>().activate();
                    }
                }
            }
        }

    }
}
