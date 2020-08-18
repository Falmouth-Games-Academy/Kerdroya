using UnityEngine;

// Input.GetTouch example.
//
// Attach to an origin based cube.
// A screen touch moves the cube on an iPhone or iPad.
// A second screen touch reduces the cube size.

public class MoveOnTouch : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;
    private RaycastHit hit;
    public ShowDetails showDetails;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Start()
    {
        if (GameObject.Find("ShowDetails"))
        {
            showDetails = GameObject.Find("ShowDetails").GetComponent<ShowDetails>();
        }

    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    Touch touch = Input.GetTouch(i);
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        showDetails.selectedObject = gameObject;
                        transform.position = ray.GetPoint(10);
                    }
                }
            }
        }
    }
}

