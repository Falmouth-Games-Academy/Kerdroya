using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMinigame : MonoBehaviour
{
    public GameObject indicator;
    public Transform CircleCenter;
    public float radius = 3f;
    public float minDragDistance = 1f;
    public ElementDragger dragger;
    public TransitionHandler THandler;
    public bool[] activegate = new bool[3];
    int startID = 0;
    private bool fadeImage;
    public GameObject overlayImage;
    private float totalFadeInValue = 0;
    public float fadeInSpeed = 0.01f;
    public GameObject draggedObject;
    public TrailRenderer trail;

    private void Update()
    {
        if (THandler.sceneState == 3) // prevents indicator from getting trapped, if Transition handler scene system is changed, this value will need updating
        {
            if (fadeImage)
            {
                
                FadeImage();
            }

            //if close to outer circle
            else if (Vector3.Distance(indicator.transform.position, dragger.targetObject.transform.position) < minDragDistance)
            {
                //move outer circle about inner circle according to mouse position (dragger object)
                indicator.transform.position = GetPointOnRadius(dragger.targetObject.transform, CircleCenter, radius);
            }
        }
    }

    public void FadeImage()
    {
        if (overlayImage.active == false)
        {
            //begin fade in of background object
            overlayImage.SetActive(true);
            Renderer myRenderer = overlayImage.GetComponent<SpriteRenderer>();
            myRenderer.material.color = new Color(1, 1, 1, 0f);
            //remove minigame obejcts
            trail.Clear();
            draggedObject.SetActive(false);
            CircleCenter.gameObject.SetActive(false);
        }
        totalFadeInValue += fadeInSpeed;

        if (totalFadeInValue < 1f)
        {
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
        else if (totalFadeInValue > 3f)
        {
            //complete game, move scene handler phase
            THandler.sceneState = 4;
        }
    }

    public void updateTrigger(int triggerID)
    {
        if (!activegate[0] && !activegate[1] && !activegate[2])
        {
            startID = triggerID;
        }
        activegate[triggerID] = true;

        if (activegate[0] && activegate[1] && activegate[2] && startID == triggerID) {
            fadeImage = true;
        }
    }


    public Vector3 GetPointOnRadius( Transform target, Transform centre, float radius)
    {
        Vector3 point= centre.InverseTransformPoint(target.position);
        return centre.TransformPoint(point.normalized * radius);
    }
}
