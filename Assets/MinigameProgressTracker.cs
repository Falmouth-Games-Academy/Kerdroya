using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameProgressTracker : MonoBehaviour
{
    public TransitionHandler THandler;
    public int points = 0;
    public int maxPoints = 4;

    public void LateUpdate()
    {
        if (points > maxPoints) {
            THandler.sceneState = 4; // WARNING this may need updating if the order in TransitionHandler is changed
        }
    }
}
