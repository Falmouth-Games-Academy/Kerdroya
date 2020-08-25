using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapPastMenu : MonoBehaviour
{
    public Button SceneSelectButton;
    public int scene; //BUILD SETTING ID, thats file -> build settings


    void Start()
    {
        SceneSelectButton.onClick.AddListener(() => ChangeScene(scene));
    }


    public void ChangeScene(int input)
    {
        Debug.Log("ASS");
        UnityEngine.SceneManagement.SceneManager.LoadScene(input);
    }
}
