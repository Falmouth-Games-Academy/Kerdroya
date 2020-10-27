using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearBehaviour : MonoBehaviour
{

    public Transform spawnLocation;
    public MinigameProgressTracker minigameProgressTracker;
    public SpriteRenderer riverQuarter;
    public SpriteRenderer riverHalf;
    public SpriteRenderer riverThreeQuarters;
    public SpriteRenderer riverFull;



    public float speed = 2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Hit")
        {
            Debug.Log("points " + minigameProgressTracker.points);
            minigameProgressTracker.points++;
        }

        switch (minigameProgressTracker.points)
        {

            case 1: riverQuarter.enabled = true;
                break;
            case 2: riverHalf.enabled = true;
                break;
            case 3: riverThreeQuarters.enabled = true;
                break;
            case 4: riverFull.enabled = true;
                break;
        }
        transform.position = spawnLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
