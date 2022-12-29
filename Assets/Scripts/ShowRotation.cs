using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRotation : MonoBehaviour
{
    //public bool tilt
    public float tiltBacklimitAngle = 30;
    public float tiltForwardslimitAngle = -15;
    public float distance;
    public float onePercentOfDistance;
    public float percentage;

    float xTilt = 0;
    public float tiltPercentage;

    GUIStyle style = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        style.fontSize = 50;
        distance = Mathf.Abs(tiltBacklimitAngle - tiltForwardslimitAngle);
        onePercentOfDistance = 100 / distance;
    }

    // Commenting out GUI debug text.
    //void OnGUI()
    //{
    //    GUI.Label(new Rect(50, 100, 200, 100), "" + transform.localRotation.eulerAngles.x, style);
    //    GUI.Label(new Rect(50, 150, 200, 100), "" + Mathf.DeltaAngle(transform.localRotation.eulerAngles.x,30),style);
    //    GUI.Label(new Rect(50, 200, 200, 100), "" + tiltPercentage, style);
    //}

    // Update is called once per frame
    void Update()
    {
       xTilt = Mathf.DeltaAngle(transform.localRotation.eulerAngles.x, tiltBacklimitAngle);
       tiltPercentage = onePercentOfDistance * xTilt;
       tiltPercentage = tiltPercentage / 100;
    }
}
