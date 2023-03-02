using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToScene : MonoBehaviour
{
    [SerializeField] private int scene;


    public void OnClick()
    {
        print("Should load scene " + Time.realtimeSinceStartup);
        SceneManager.LoadScene(scene);
    }

}
