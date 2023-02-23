using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SandGameScript))]
public class SandRopeFade : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SandGameScript sandGameScript; //This needs to be the 'distance' value from the sand puzzle script
    [SerializeField] private Vignette vignette;
    public bool halfway = false;
    private bool flashed = false;

    // Start is called before the first frame update
    void Start()
    {
        sandGameScript = GetComponent<SandGameScript>();
        halfway = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sandGameScript.distance >= .95f && halfway == false)
        {
            halfway = true;
        }

        if (halfway == false)
        {
            spriteRenderer.color = new Color(1, 1, 1, sandGameScript.distance / 2);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, (1 - (sandGameScript.distance / 2)));
            if(sandGameScript.distance <= 0.1f && !flashed)
            {
                vignette?.FlashVignette();
                flashed = true;
            }
        }
    }
}
