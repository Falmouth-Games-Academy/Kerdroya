using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    public GameObject interTitle;
    public GameObject fadeOutObject;
    public GameObject WindPuzzle;
    public AudioManager AManager;
    public Camera cam;

    public int sceneState = 0;
    public int puzzleID = -1;// each puzzle has associated ID, see scene number

    //Scene one
    private float alphaDelta = 0f;
    public float alphaTranitionSpeed = 0.05f; //speed of fade out on intertitle

    //Scene two
    public GameObject findObjectTarget; // GameObject containing hidden
    public float findObjectRollOverTime = 1000;//time taken before object location recognised
    public float timeWaited = 0;

    //Scene three
    public CanvasGroup promptCanvasGroup;
    public float waitTime = 3;
    public float timeElapsed = 0;

    //Scene four
    public float puzzleFadeInSpeed = 0.05f;
    private float totalFadeInValue = 0f;

    //StateFive
    bool coRoutineStarted = false;

    // internal globals
    private Renderer myRenderer;

    private void Start()
    {
        WindPuzzle.SetActive(false);
        myRenderer = findObjectTarget.GetComponent<Renderer>();
        fadeOutObject.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, 0);
        promptCanvasGroup.alpha = 0;
    }

    void Update()
    {
        switch (sceneState)
        {
            case 0: StateZero(); break;//show and wait for intertitle
            case 1: StateOne(); break;//transition out of intertitle
            case 2: StateTwo(); break;//find object in scene, used by FreeCamera script
            case 3: StateThree(); break;//Display puzzle instructions/prompt
            case 4: StateFour(); break;//Transition to minigame, used by circle minigame (colliford lake)
            case 5: StateFive(); break;//Puzzle completed, Will finishes his audio clips 
            case 6: StateSix(); break; //End, triggered externally by minigame progress tracker
        }
    }

    private void StateZero()
    { //show intertitle, wait for end of interititle

        AManager.PlayClipOnce(0);

        if (!AManager.audioSource.isPlaying)
        {
            sceneState = 1;
        }

    }

    private void StateOne() //transition out of intertitle
    {
        alphaDelta += alphaTranitionSpeed;
        interTitle.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, Mathf.SmoothStep(1.0f, 0.0f, alphaDelta));
        if (alphaDelta > 0.95f)
        {
            alphaDelta = 0;
            interTitle.SetActive(false);
            sceneState = 2;
        }
    }

    private void StateTwo() //find object in scene
    {
        AManager.PlayClipOnce(1);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            // get hit game object and show text if it has any (this must be the first child object)
            GameObject hitGameObject = hit.transform.gameObject;
            Debug.Log(hitGameObject.name);
            hitGameObject.transform.GetChild(0).gameObject.SetActive(true);

            // check if the answer is right and respond accordingly
            if (hit.transform.gameObject.tag.Equals("right"))
            {
                ActivateGame(hitGameObject);
                return;
            }

            // check if the answer is wrong and respond accordingly
            if (hit.transform.gameObject.tag.Equals("wrong")) {
                playAudio("WrongAudio");
                StartCoroutine(destroyAfter(hitGameObject, 2));
            }
        }

        timeWaited = 0;
        resetRightGamesObects();
    }

    private void ActivateGame(GameObject hitGameObject)
    {
        playAudio("RightAudio");
        Renderer hitGameObjectRenderer = hitGameObject.GetComponent<Renderer>();
        hitGameObjectRenderer.material.color = new Color(1, 1, 1, timeWaited / findObjectRollOverTime);

        // check to see if we need to move to the next state
        timeWaited += Time.deltaTime;
        if (timeWaited > findObjectRollOverTime)
        {
            resetRightGamesObects();
            sceneState = 3;
        }
        return;
    }

    IEnumerator destroyAfter(GameObject gameObject, int delay)
    {
        Debug.Log("DESTROY");
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void resetRightGamesObects() {
        GameObject[] rightGameObjects = GameObject.FindGameObjectsWithTag("right");
        foreach (GameObject gameObject in rightGameObjects)
        {
            Renderer gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObjectRenderer.material.color = new Color(1, 1, 1, 0.01f);
        }
    }

    private void playAudio(string name) {
        Debug.Log("AUDIO");
        AudioSource audio = GameObject.Find(name).GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying) {
            audio.Play();
        } else {
            Debug.Log("can't find");
        }
    }

    private void StateThree()
    {
        //This should display the intructions / prompt when we have them - presumably on a canvas
        //The user will advance to the next state by tapping through the prompt of similar


        AManager.PlayClipOnce(2);
        promptCanvasGroup.alpha = 1;

        if (timeElapsed < waitTime)
        {
            
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
        }
        if (promptCanvasGroup.alpha == 1 && timeElapsed >= waitTime && !AManager.audioSource.isPlaying)
        {
            promptCanvasGroup.alpha = 0;
            sceneState = 4;
        }

    }


private void StateFour() //Transition to game
    {
        //SWitch the camera to orthographic for the 2D game - we may need to switch this back for subsequent plays
        cam.orthographic = true;

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

    public void StateFive()
    {
        if (AManager.coRoutineEnded == false)
        {
            AManager.PlayClipsToEnd(3);
        }

        if (AManager.coRoutineEnded == true && AManager.audioSource.isPlaying == false)
        {
            sceneState = 6;
        }

    }

    private void StateSix()
    {
        alphaDelta += alphaTranitionSpeed;
        fadeOutObject.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, Mathf.SmoothStep(1.0f, 0.0f, 1-alphaDelta));
        if (alphaDelta > 0.95f)
        {
            AppProgression.levelCompleted[puzzleID] = true;
            AppProgression.currentComplete = puzzleID;
            UnityEngine.SceneManagement.SceneManager.LoadScene(8);
        }
    }
}