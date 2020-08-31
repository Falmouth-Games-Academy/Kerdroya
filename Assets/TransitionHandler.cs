using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    public GameObject interTitle;
    public GameObject WindPuzzle;
    public AudioManager AManager;
    public Camera cam;

    public int sceneState = 0;

    private float alphaDelta = 0f;
    public float alphaTranitionSpeed = 0.05f; //speed of fade out on intertitle

    public GameObject findObjectTarget; // GameObject containing hidden
    public float findObjectRollOverTime = 1000;//time taken before object location recognised
    public float timeWaited = 0;

    public float puzzleFadeInSpeed = 0.05f;
    private float totalFadeInValue = 0f;

    private Renderer myRenderer;

    private void Start()
    {
        WindPuzzle.SetActive(false);
        myRenderer = findObjectTarget.GetComponent<Renderer>();
    }

    void Update()
    {
        switch (sceneState)
        {
            case 0: EntryScene(); break;
            case 1: SceneOne(); break;
            case 2: SceneTwo(); break; // used by FreeCamera script
            case 3: SceneThree(); break;
        }
    }

    private void EntryScene()
    { //show intertitle, wait for end of interititle
        if (AManager.audioEnded)
        {
            sceneState = 1;
        }
    }

    private void SceneOne() //transition out of intertitle
    {
        alphaDelta += alphaTranitionSpeed;
        interTitle.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, Mathf.SmoothStep(1.0f, 0.0f, alphaDelta));
        if (alphaDelta > 0.95f)
        {
            interTitle.SetActive(false);
            sceneState = 2;
        }
    }

    private void SceneTwo() //find object in scene
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            if (hit.transform.Equals(findObjectTarget.transform))
            {
                myRenderer.material.color = new Color(1, 1, 1, timeWaited/findObjectRollOverTime);

                timeWaited += Time.deltaTime;
                if (timeWaited > findObjectRollOverTime)
                {
                    sceneState = 3;
                }
            }
            else
            {
                RayNotHitTarget();
            }
        }
        else
        {
            RayNotHitTarget();
        }
    }

    private void RayNotHitTarget()
    {
        myRenderer = findObjectTarget.GetComponent<Renderer>();
        myRenderer.material.color = new Color(1, 1, 1, 0.01f);
        timeWaited = 0;
    }

    private void SceneThree() //Transition to game
    {
        if (!WindPuzzle.active)
        {
            WindPuzzle.SetActive(true);
            foreach (GameObject fadeTarget in GameObject.FindGameObjectsWithTag("PuzzleComponent"))
            {
                if (fadeTarget.GetComponent<Renderer>() != null)
                {
                    Renderer myRenderer = fadeTarget.GetComponent<Renderer>();
                    myRenderer.material.color = new Color(1, 1, 1, 0f);
                }
                if (fadeTarget.GetComponent<SpriteRenderer>() != null)
                {
                    Renderer myRenderer = fadeTarget.GetComponent<SpriteRenderer>();
                    myRenderer.material.color = new Color(1, 1, 1, 0f);
                }
            }

        }
        if (totalFadeInValue < 1f)
        {
            totalFadeInValue += puzzleFadeInSpeed;
            if (totalFadeInValue >= 0.99f) { totalFadeInValue = 1.0f; }
            foreach (GameObject fadeTarget in GameObject.FindGameObjectsWithTag("PuzzleComponent"))
            {
                if (fadeTarget.GetComponent<Renderer>() != null)
                {
                    fadeTarget.GetComponent<Renderer>().material.color = new Color(1, 1, 1, totalFadeInValue);
                }
                if (fadeTarget.GetComponent<SpriteRenderer>() != null)
                {
                    fadeTarget.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, totalFadeInValue);
                }
            }
        }
    }
}