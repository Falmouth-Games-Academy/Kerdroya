using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{
    public bool leftFacing;
    public float speed = 1.5f;
    public int state = 0; //0 flying, 1 going to flower, 2 leaving.
    public float startYpos = 0;
    float rand;
    public GameObject Flower;
    private MinigameProgressTracker progressTracker;
    // Start is called before the first frame update
    void Start()
    {
        Flower = GameObject.Find("Flower");
        progressTracker = GameObject.Find("GameManager").GetComponent("MinigameProgressTracker") as MinigameProgressTracker;
        rand = Random.Range(1f, 10f); //This random value get's added to the sine wave so each bee is unique
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "BeeGenerator":
                GameObject.Destroy(gameObject);
                break;

            case "FlowerZone":
                if (state == 0 && Flower != null)
                {
                    state = 1;
                }
                
                break;
            default:
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0: //moving, not within flower radius
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
            case 1: // within flower radius
                transform.position = Vector3.MoveTowards(transform.position, Flower.transform.position, 0.02f);
                if (Vector3.Distance(transform.position, Flower.transform.position) < 0.1f)
                {
                    progressTracker.points++;
                    GameObject.Destroy(gameObject);
                }
                break;
            case 3:
                break;
            default:
                break;
        }

        
    }
}
