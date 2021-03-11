using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{  
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool[] clipsPlayed;
    public bool coRoutineStarted = false;
    public bool coRoutineEnded = false;

    //private int playOrder = 0;

    //public bool audioQueued = false;

    //public  bool audioEnded = false; //

    // Start is called before the first frame update
    void Start()
    {
        clipsPlayed = new bool[audioClips.Length];
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        */
    }
    /*
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
    */

    public void PlayClip(int clip)
    {
        if (clip < 0 || clip > audioClips.Length)
        {
            return;
        }

        audioSource.clip = audioClips[clip];
        audioSource.Play();
    }

    public void PlayClipOnce(int clip)
    {
        if (clip < 0 || clip > audioClips.Length)
        {
            return;
        }

        if (!clipsPlayed[clip])
        {
            audioSource.clip = audioClips[clip];
            audioSource.Play();
            clipsPlayed[clip] = true;
        }
        
    }

    public void PlayClipsToEnd(int startingClip)
    {
       
        if (coRoutineStarted == false)
        {
            StartCoroutine(PlayClipsToEndCoroutine(startingClip));
            coRoutineStarted = true;
        }
    }

    public IEnumerator PlayClipsToEndCoroutine(int startingClip)
    {
        if (startingClip < 0 || startingClip > audioClips.Length)
        {
            yield return null;
        }

        int currentClip = startingClip;

        while(currentClip < audioClips.Length)
        {
            if (audioSource.isPlaying)
            {
                yield return new WaitForSeconds(1);
            }

            if (!audioSource.isPlaying)
            {
                PlayClip(currentClip);
                currentClip++;
            }
        }
        coRoutineEnded = true;
    }
}
