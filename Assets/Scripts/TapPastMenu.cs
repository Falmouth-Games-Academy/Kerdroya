using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPastMenu : MonoBehaviour
{
    public void OnMouseDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
