using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToScene : MonoBehaviour
{
    [SerializeField] private int scene;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, -1))
                print("Raycast hit: " + hit.collider.gameObject.name);
            else print("Raycast didn't hit anything");

        }
    }
    
    private void OnMouseOver()
    {
        print("Mouse is over button");
    }

    public void OnClick()
    {
        print("Should load scene " + Time.realtimeSinceStartup);
        SceneManager.LoadScene(scene);
    }

}
