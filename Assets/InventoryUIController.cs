using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//!!    ASSUMES APP PROGRESSION ORDER = WINDOW HIERARCHY ORDER  !!
public class InventoryUIController : MonoBehaviour
{
    public GameObject StainedWindowPanel;
    public GameObject fadeInObject;

    private float alphaDelta = 1;
    public float alphaTransitionSpeed = 0.05f;

    private Transform[] children;
    private int sceneState = -1;

    void Start()
    {
        startProcedure();
    }

    private void Update()
    {

        switch (sceneState)
        {
            case -1: DefaultAction(); break;
            case  0: FadeInWhiteout(); break;
            case  1: FadeInPiece(); break;
            case  2: FadeInWindow(); break;
            case  3: sceneState = -1; AppProgression.currentComplete = -1; break;
        }
    }

    private void DefaultAction()
    {
        //go to end scene is all levels complete
        if (AppProgression.levelCompleted.All(x => x)) UnityEngine.SceneManagement.SceneManager.LoadScene(15);
    }

    private void FadeInWhiteout()
    {
        alphaDelta -= alphaTransitionSpeed;
        fadeInObject.GetComponent<UnityEngine.UI.RawImage>().color = new Color(1, 1, 1, Mathf.SmoothStep(1.0f, 0.0f, 1-alphaDelta));
        if (alphaDelta < 0.05f)
        {
            alphaDelta = 0;
            fadeInObject.SetActive(false);
            sceneState = 1;
        }
    }

    private void FadeInPiece()
    {
        alphaDelta += alphaTransitionSpeed;
        children[AppProgression.currentComplete + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alphaDelta);
        if (alphaDelta > 0.95f)
        {

            children[AppProgression.currentComplete + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            sceneState = 2;
            renderWindowPieces();
            setWindowAlphaWithException(0, AppProgression.currentComplete);
            alphaDelta = 0;
        }
    }

    private void FadeInWindow()
    {
        alphaDelta += alphaTransitionSpeed;
        setWindowAlphaWithException(alphaDelta, AppProgression.currentComplete);
        if (alphaDelta > 0.95f)
        {
            setWindowAlphaWithException(1, AppProgression.currentComplete);
            sceneState = 3;
        }
    }

    void renderWindowPieces()
    {
        //set background window
        children[1].gameObject.SetActive(true);
        for (int count = 0; count < AppProgression.levelCompleted.Length; count++)
        {
            //count+2 used to account for background image and parent's own transform
            children[count + 2].gameObject.SetActive(AppProgression.levelCompleted[count]);
        }
    }

    void setWindowAlphaWithException(float alpha, int exception)
    {
        //set background window
        children[1].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alpha);
        for (int count = 0; count < AppProgression.levelCompleted.Length; count++)
        {
            if (count != exception)
            {
                //count+2 used to account for background image and parent's own transform
                children[count + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alpha);
            }
        }
    }

    void startProcedure()
    {
        children = StainedWindowPanel.GetComponentsInChildren<Transform>();
        if (AppProgression.currentComplete != -1)
        { //render most recently unlocked frame first
            for (int count = 0; count < AppProgression.levelCompleted.Length + 1; count++)
            {//remove background image and all children
                children[count + 1].gameObject.SetActive(false);
            }

            children[AppProgression.currentComplete + 2].gameObject.SetActive(true);
            children[AppProgression.currentComplete + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
            //make pane invisible at startup
            children[1].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
            sceneState = 0;
        }
        else
        {
            fadeInObject.SetActive(false);
            renderWindowPieces();
        }
    }
}
