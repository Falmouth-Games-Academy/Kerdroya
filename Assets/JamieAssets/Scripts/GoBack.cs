using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(0);
    }
}
