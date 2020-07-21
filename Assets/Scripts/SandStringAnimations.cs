using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sigil))]
public class SandStringAnimations : MonoBehaviour
{

    public Sigil sigil;
    public int progressNumber;
    public Animator SandString1;
    public Animator SandString2;
    public Animator SandString3;
    public Animator SandString4;

    // Start is called before the first frame update
    void Start()
    {
        sigil = GetComponent<Sigil>();
    }

    // Update is called once per frame
    void Update()
    {
        progressNumber = sigil.progressNumber;

        switch (progressNumber)
        {
            case 3:
                SandString1.enabled = true;
                break;
            case 5:
                SandString2.enabled = true;
                break;
            case 7:
                SandString3.enabled = true;
                break;
            case 9:
                SandString4.enabled = true;
                break;
        }

    }
}
