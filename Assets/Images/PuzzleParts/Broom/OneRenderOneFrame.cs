using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//??? what and why is this script???
public class OneRenderOneFrame : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("staret");
       // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnPostRender()
    {
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
