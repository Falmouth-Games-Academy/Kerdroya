using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameProgressTracker : MonoBehaviour
{
    public TransitionHandler THandler;
    public int points = 0;
    public int maxPoints = 4;
    public Vignette vignette;

    public void LateUpdate()
    {
        if (points >= maxPoints && THandler.sceneState < 5) 
        {
            vignette?.FlashVignette();
            THandler.sceneState = 5; // WARNING this may need updating if the order in TransitionHandler is changed
        }
    }
}
