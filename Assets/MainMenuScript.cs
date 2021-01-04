using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Image SplashImage;
    public GameObject SplashGameObject;
    public GameObject SplashTextObject;
    public Text[] fadeOutText;
    public Text[] fadeInText;
    
    public float waitPeriod = 1f;
    public float fadeOutSpeed = 0.05f;
    private float totalFadeOutValue = 1;

    public void Start()
    {
        string result = string.Join(", ", AppProgression.levelCompleted.Select(x => x.ToString()).Aggregate((x, y) => x + ", " + y));
        Debug.Log(result);

        if (AppProgression.openingwatched)
        {
            SplashGameObject.SetActive(false);
            SplashTextObject.SetActive(false);
            foreach (Text text in fadeInText)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            }
        }
    }

    public void Update()
    {
        waitPeriod -= Time.deltaTime;
        if (waitPeriod < 1 && SplashImage.IsActive())
        {
            if (SplashImage.color.a < 0.01f) {
                SplashGameObject.SetActive(false);
                AppProgression.openingwatched = true;
            }
            else
            {
                SplashImage.color = new Color(SplashImage.color.r, SplashImage.color.g, SplashImage.color.b, SplashImage.color.a - (totalFadeOutValue*Time.deltaTime));
                foreach (Text text in fadeOutText)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (totalFadeOutValue * Time.deltaTime));
                }
            }
        }
        if (waitPeriod < 0)
        {
            foreach (Text text in fadeInText)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + fadeOutSpeed);
            }
        }
    }
}
