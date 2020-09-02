using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!!    ASSUMES APP PROGRESSION ORDER = WINDOW HIERARCHY ORDER  !!
public class InventoryUIController : MonoBehaviour
{
    public GameObject StainedWindowPanel;

    private float alphaDelta = 0;
    public float alphaTransitionSpeed = 0.05f;

    private Transform[] children;
    private int sceneState = -1;

    void Start()
    {
        children = StainedWindowPanel.GetComponentsInChildren<Transform>();

        if (AppProgression.currentComplete != -1)
        { //render most recently unlocked frame first
            Debug.Log("CurrentComplete = " + AppProgression.currentComplete);

            for (int count = 0; count < AppProgression.levelCompleted.Length+1; count++)
            {//remove background image and all children
                children[count + 1].gameObject.SetActive(false);
            }

            children[AppProgression.currentComplete + 2].gameObject.SetActive(true);
            sceneState = 0;
        }
        else
        {
            renderWindowPieces();
        }
    }

    private void Update()
    {

        switch (sceneState)
        {
            case 0: FadeInPiece(); break;
            case 1: FadeInWindow(); break;
        }
    }

    private void FadeInPiece()
    {
        alphaDelta += alphaTransitionSpeed;
        children[AppProgression.currentComplete + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, alphaDelta);
        if (alphaDelta > 0.95f)
        {

            children[AppProgression.currentComplete + 2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            sceneState = 1;
            renderWindowPieces();
            setWindowAlphaWithException(0, AppProgression.currentComplete);
            alphaDelta = 0;
        }
    }

    private void FadeInWindow()
    {
        Debug.Log("AYYOO");
        alphaDelta += alphaTransitionSpeed;
        setWindowAlphaWithException(alphaDelta, AppProgression.currentComplete);
        if (alphaDelta > 0.95f)
        {
            setWindowAlphaWithException(1, AppProgression.currentComplete);
            sceneState = -1;
        }
    }

    void renderWindowPieces()
    {
        children[1].gameObject.SetActive(true);
        for (int count = 0; count < AppProgression.levelCompleted.Length; count++)
        {
            //count+2 used to account for background image and parent's own transform
            children[count + 2].gameObject.SetActive(AppProgression.levelCompleted[count]);
        }
    }

    void setWindowAlphaWithException(float alpha, int exception)
    {
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
}
