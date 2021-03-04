using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class ParkHeadTap : MonoBehaviour, IPointerClickHandler
{

    public MinigameProgressTracker miniGameProgressTracker;


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            miniGameProgressTracker.points++;
        }
        
    }
}
