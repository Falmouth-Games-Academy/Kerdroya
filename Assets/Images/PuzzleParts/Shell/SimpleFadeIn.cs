using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleFadeIn : MonoBehaviour
{

    public bool started = false;
    float fadePercent = 0;
    public MinigameProgressTracker miniGameProgressTracker;

    // Update is called once per frame
    void Update()
    {
        if (fadePercent >= 1 && miniGameProgressTracker != null)
        {
            miniGameProgressTracker.points = 99;
        }

        if (started)
        {
            fadePercent += 0.1f;

            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        }
    }
}
