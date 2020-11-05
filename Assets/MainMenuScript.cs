using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Image SplashImage;
    public GameObject SplashGameObject;
    
    public float waitPeriod = 1f;
    public float fadeOutSpeed = 0.05f;

    private float totalFadeOutValue = 1;

    public void Start()
    {
        if (AppProgression.openingwatched)
        {
            SplashGameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (waitPeriod > 0)
        {
            waitPeriod -= Time.deltaTime;
        }
        else
        {
            if (totalFadeOutValue < 0f) {
                SplashGameObject.SetActive(false);
                AppProgression.openingwatched = true;
            }
            else
            {
                totalFadeOutValue -= fadeOutSpeed;
                SplashImage.color = new Color(1, 1, 1, totalFadeOutValue);
            }
        }
    }
}
