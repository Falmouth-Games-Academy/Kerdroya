using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private int playOrder = 0;



    private bool audioQueued = false;

    public  bool audioEnded = false; //

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioEnded)
        {
            if (!audioSource.isPlaying && playOrder == 0)
            {
                audioEnded = true;
                AdvancePlayOrder();
            }

            if (audioQueued)
            {
                audioQueued = false;
                AdvancePlayOrder();
            }
        }
    }

    public void AdvancePlayOrder()
    {
        if (playOrder <= audioClips.Length)
        {
            if (audioSource.isPlaying)
            {
                audioQueued = true;
                return;
            }

            playOrder++;
            audioSource.clip = audioClips[playOrder];
            audioSource.Play();
        }
    }
}
