using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigateBySceneName : MonoBehaviour
{
    public Button SceneSelectButton;
    public string sceneName;


    void Start()
    {
        SceneSelectButton.onClick.AddListener(() => ChangeScene(sceneName));
    }


    public void ChangeScene(string input)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(input);
    }
}
