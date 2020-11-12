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
    public float puzzleFadeInSpeed = 0.05f;
    private float totalFadeInValue = 0f;
    //scene four
    
    // internal globals
    private Renderer myRenderer;

    private void Start()
    {
        WindPuzzle.SetActive(false);
        myRenderer = findObjectTarget.GetComponent<Renderer>();
        fadeOutObject.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        switch (sceneState)
        {
            case 0: EntryScene(); break;//show and wait for intertitle
            case 1: SceneOne(); break;//transition out of intertitle
            case 2: SceneTwo(); break;//find object in scene, used by FreeCamera script
            case 3: SceneThree(); break;//Transition to minigame, used by circle minigame (colliford lake)
            case 4: SceneFour(); break; //End, triggered externally by minigame progress tracker
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
            alphaDelta = 0;
            interTitle.SetActive(false);
            sceneState = 2;
        }
    }

    private void SceneTwo() //find object in scene
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            // get hit game object and show text if it has any (this must be the first child object)
            GameObject hitGameObject = hit.transform.gameObject;
            Debug.Log(hitGameObject.name);
            hitGameObject.transform.GetChild(0).gameObject.SetActive(true);
            
            // check if the answer is right and respond accordingly
            if(hit.transform.gameObject.tag.Equals("right"))
            {
                NewMethod(hitGameObject);
                return;
            }

            // check if the answer is wrong and respond accordingly
            if (hit.transform.gameObject.tag.Equals("wrong")){
                playAudio("WrongAudio");
            }   
        }

        timeWaited = 0;
        resetRightGamesObects();
    }

    private void NewMethod(GameObject hitGameObject)
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

    private void resetRightGamesObects() {
        GameObject[] rightGameObjects = GameObject.FindGameObjectsWithTag("right");
        foreach (GameObject gameObject in rightGameObjects)
        {
            Renderer gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObjectRenderer.material.color = new Color(1, 1, 1, 0.01f);
        }
    }

    private void playAudio (string name) {
        Debug.Log("AUDIO");
        AudioSource audio = GameObject.Find(name).GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying){
            audio.Play();
        }else{
            Debug.Log("can't find");
        }
    }

    private void SceneThree() //Transition to game
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

    private void SceneFour()
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