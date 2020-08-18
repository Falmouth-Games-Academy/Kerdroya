using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class WindBehaviour : MonoBehaviour
{
    public float lifespan = 2;
    Animator animator;
    BoxCollider boxCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play()
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        animator.SetInteger("State", 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (lifespan > 0)
        {
            lifespan -= Time.deltaTime;
        }
        if (lifespan <= 0)
        {
            //Debug.Log("sodfhsdfh");
            animator.SetInteger("State", 1);
            
        }
    }
}
