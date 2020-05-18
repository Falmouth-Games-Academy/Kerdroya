using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    public bool gyroActive;
    private Gyroscope gyro;

    private RaycastHit hit;
    private Quaternion initalRotation;

    public LayerMask castLayer;

    void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActive = true;

            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            initalRotation = new Quaternion(0, 0, 1, 0);

        }
    }

    private void Update()
    {
        if (gyroActive == true)
        {
            transform.localRotation = gyro.attitude * initalRotation;
            if (Input.touchCount > 0)
            {
                for (var i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        if (Physics.Raycast(ray, castLayer))
                        {
                            hit.collider.GetComponent<TapObject>().activate();
                        }
                    }
                }
            }
        }
    }
}
