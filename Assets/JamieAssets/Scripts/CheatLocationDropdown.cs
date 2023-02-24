using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatLocationDropdown : MonoBehaviour
{

    private string val = "Park Head";


    private Dictionary<string, int> scenes = new Dictionary<string, int>()
    {
        {"Priests Cove", 10 },
        {"Pentargon", 5 },
        {"Clarrick Woods", 11 },
        {"Colliford Lake", 4 },
        {"West Coombe", 2 },
        {"Lucky Hole", 6 },
        {"Dennis Cove", 7 },
        {"Park Head", 14 },
        {"Predannak", 1 },
        {"Bawden", 12 },
        {"North Cliff", 3 },
        {"Blouth", 13 }
    };

    public void OnChange(Dropdown dd)
    {
        val = dd.captionText.text;
    }

    public void Clicked()
    {
        SceneManager.LoadScene(scenes[val]);
    }

}
