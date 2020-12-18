using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public GameObject target;
    public TransitionHandler THandler;

    public float mouseSensitivity = 100.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Vector3 rot = target.transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (THandler.sceneState == 2)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = -Input.GetAxis("Mouse Y");

                rotY += mouseX * mouseSensitivity * Time.deltaTime;
                rotX += mouseY * mouseSensitivity * Time.deltaTime;

                Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
                target.transform.rotation = localRotation;
            }
        }
    }
}
