using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollission : MonoBehaviour
{
    public bool hitGoal = false;

    void OnCollisionEnter(Collision colli)
    {
        if (colli.gameObject.tag == "PuzzleComponent")
        {
            hitGoal = true;
        }
    }

    private void OnCollisionExit(Collision colli)
    {
        if (colli.gameObject.tag == "PuzzleComponent")
        {
            hitGoal = false;
        }
    }
}
