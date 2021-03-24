using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioManager))]
public class ARscene_state : MonoBehaviour
{
    public AudioManager audioManager;
    public int state = 0;
    public float waitTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.PlayClipsToEnd(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            if (audioManager.coRoutineEnded == true && audioManager.audioSource.isPlaying == false)
            {
                state = 1;
            }
        }
        if (state == 1)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(8);
            }
        }

    }
}
