using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{

    public Transform targetToCopy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetToCopy != null)
        {
            transform.position = targetToCopy.position;
        }
    }
}
