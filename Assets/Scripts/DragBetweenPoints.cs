using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBetweenPoints : MonoBehaviour
{
    public GameObject targetObject;
    public LayerMask hitLayers;
    public MinigameProgressTracker MGPT;
    int state = 0;
    public SpriteRenderer SandRenderer;

    public GameObject[] source_sand;
    public GameObject[] goal_sand;


    public void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            if (Vector3.Distance(targetObject.transform.position, hit.point) < 1.5f)
            {
                targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, hit.point, 0.5f);
            }

            if(hit.transform.gameObject.name == "SAND_SOURCE")
            {
                if (!SandRenderer.enabled)
                {
                    source_sand[MGPT.points].SetActive(false);
                    Debug.Log("SOURCE");
                    state = 1;
                    SandRenderer.enabled = true;
                }
            }
            if (hit.transform.gameObject.name == "SAND_GOAL")
            {
                if (SandRenderer.enabled)
                {
                    goal_sand[MGPT.points].SetActive(true);
                    SandRenderer.enabled = false;
                    MGPT.points++;
                }
            }
        }
    }
}
