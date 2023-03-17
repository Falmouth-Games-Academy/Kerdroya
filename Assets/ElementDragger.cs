using UnityEngine;
using UnityEngine.EventSystems;

public class ElementDragger : MonoBehaviour
{
    public GameObject targetObject;
    public LayerMask hitLayers;

    public void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            if (targetObject)
                targetObject.transform.position = hit.point;
        }
    }
}