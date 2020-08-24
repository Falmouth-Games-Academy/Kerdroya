using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{
    public bool leftFacing;
    public float speed = 1.5f;
    public int state = 1; //0 flying, 1 going to flower, 2 leaving.
    public float startYpos = 0;
    float rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(1f, 10f); //This random value get's added to the sine wave so each bee is unique
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                if (leftFacing)
                {
                    float xpos = transform.localPosition.x - (speed * Time.deltaTime);
                    float ypos = Mathf.Sin(Time.time + Time.deltaTime + rand) / 10;
                    ypos += startYpos;
                    transform.localPosition = new Vector3(xpos, ypos, transform.localPosition.z);
                }
                if (!leftFacing)
                {
                    float xpos = transform.localPosition.x + (speed * Time.deltaTime);
                    float ypos = Mathf.Sin(Time.time + Time.deltaTime + rand) / 10;
                    ypos += startYpos;
                    transform.localPosition = new Vector3(xpos, ypos, transform.localPosition.z);
                }
                break;
            case 1:
                break;
            case 3:
                break;
            default:
                break;
        }

        
    }
}
