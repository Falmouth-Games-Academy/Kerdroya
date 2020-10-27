using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDistanceMeasure : MonoBehaviour
{

    public Transform a;
    public Transform b;
    public float distance;
    public float asPercentage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(a.position, b.position);
        asPercentage = distance/4;
    }
}
