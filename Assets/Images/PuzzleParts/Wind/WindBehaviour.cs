using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class WindBehaviour : MonoBehaviour
{
    public float timer;
    Animator animator;
    BoxCollider boxCollider;
    public MinigameProgressTracker MGPT;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        timer = Random.Range(2.0f, 6.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (animator.GetInteger("State") != 2)
        {
            MGPT.points++;
            animator.SetInteger("State", 2);
        }
    }

    void ResetTimer()
    {
       
        timer = Random.Range(2.0f, 6.0f);
        animator.SetInteger("State", 0);
        animator.Play("WindAnimation");

    }

    // Update is called once per frame
    void Update()
    {
        // check to see whether wind should stop
        if (MGPT.points >= MGPT.maxPoints) Destroy(this);


        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            ResetTimer();
        }
    }
}
