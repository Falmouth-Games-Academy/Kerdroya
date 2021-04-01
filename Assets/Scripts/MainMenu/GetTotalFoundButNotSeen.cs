using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetTotalFoundButNotSeen : MonoBehaviour
{
    Text counter;
    public GameObject ui; 
    
    // Start is called before the first frame update
    void Awake ()
    {
        if (AppProgression.countNewFactoids() > 0) {
            counter = GetComponent<Text>();
            counter.text = "" + AppProgression.countNewFactoids();
        } else {
            ui.SetActive(false);
        }
        

    }
}
