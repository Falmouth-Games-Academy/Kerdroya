using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class WallPieceScript : MonoBehaviour
{
    public MinigameProgressTracker MGPT;
    Animator animator;

    private bool isTriggered = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!isTriggered)
        {
            animator.Play("Rock");
            MGPT.points++;
            isTriggered = true;
        }
    }

}
