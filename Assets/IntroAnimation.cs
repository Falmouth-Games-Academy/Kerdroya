using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour
{
    public AudioManager audioManager;
    public enum IntroStates
    {
        Zero,
        One,//state description
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten
    };

    public List<Graphic> graphics;
    public bool[] graphicsFaded;

    public IntroStates introState;
    public float waitTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        graphicsFaded = new bool[graphics.Count];
        for (int i = 0; i < graphics.Count; i++)
        {
            graphics[i].canvasRenderer.SetAlpha(0);
        }


    }

    public void FadeGraphicOnce(int graphicElement, float targetAlpha, float duration)
    {
        if (graphicElement < 0 || graphicElement > graphics.Count)
        {
            return;
        }

        if (!graphicsFaded[graphicElement])
        {
            graphics[graphicElement].CrossFadeAlpha(targetAlpha, duration, false);
            graphicsFaded[graphicElement] = true;
        }

    }

    //Parameters: When this IMAGE has an alpha of TARGETVALUE start a new crossfade with the parameters NEXTIMAGE, NEXTVALUE,NEXTTIME, and advance to the NEXTSTATE 
    void WhenGraphicFaded(Graphic graphic, float targetAlpha, Graphic newGraphic, float nextAlpha, float nextTime, int nextState)
    {
        if (graphic.canvasRenderer.GetAlpha() == targetAlpha)
        {
            newGraphic.CrossFadeAlpha(nextAlpha, nextTime, false);
            introState = (IntroStates)nextState;
        }
    }

    void WhenGraphicFaded(Graphic graphic, float targetAlpha, Graphic newGraphic, float nextAlpha, float nextTime)
    {
        if (graphic.canvasRenderer.GetAlpha() == targetAlpha)
        {
            newGraphic.CrossFadeAlpha(nextAlpha, nextTime, false);
        }
    }

    void WhenGraphicFaded(Graphic graphic, float targetAlpha, int nextState)
    {
        if (graphic.canvasRenderer.GetAlpha() == targetAlpha)
        {
            introState = (IntroStates)nextState;
        }
    }

    void WhenTimeElapsed(float wait, int nextState)
    {
        if (waitTimer < wait)
        {
            waitTimer += Time.deltaTime;     
        }
        else
        {
            waitTimer = 0;
            introState = (IntroStates)nextState;
        }
    }

    void WhenTimeElapsed(float wait, Graphic nexGraphic, float nextAlpha, float nextTime, int nextState)
    {
        if (waitTimer < wait)
        {
            waitTimer += Time.deltaTime; 
        }
        if (waitTimer >= wait)
        {
            nexGraphic.CrossFadeAlpha(nextAlpha, nextTime, false);
            introState = (IntroStates)nextState;
        }
    }

    void WhenAudioEndedChangeState(AudioManager am, int clipID, int nextState)
    {
        if (am.clipsPlayed.Length >= clipID)
        {
            if (am.clipsPlayed[clipID] == true)
            {
                introState = (IntroStates)nextState;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (introState)
        {
            case (IntroStates)0:
                audioManager.PlayClipOnce(1);
                FadeGraphicOnce(0, 1, 5);//Fade the window in
                introState = (IntroStates)1;
                break;

            case (IntroStates)1:
                WhenGraphicFaded(graphics[0], 1, 2);//When window faded in advance to state 2
                break;

            case (IntroStates)2:
                WhenTimeElapsed(2f, 3);
                break;

            case (IntroStates)3:
                introState = (IntroStates)4;
                break;

            case (IntroStates)4:
                FadeGraphicOnce(1, 1, 3f);
                introState = (IntroStates)5;
                break;

            case (IntroStates)5:
                WhenGraphicFaded(graphics[1], 1, graphics[2], 1, 2f);//When Shadow1 is in, start fading in Shadow2
                WhenGraphicFaded(graphics[1], 1, graphics[1], 0, 2f);//When Shadow1 is in, fade it out
                WhenGraphicFaded(graphics[2], 1, graphics[3], 1, 2f);//When Shadow2 is in, start fading in Shadow3
                WhenGraphicFaded(graphics[2], 1, graphics[2], 0, 2f);//When Shadow2 is in, fade it out
                WhenGraphicFaded(graphics[3], 1, 6);//When shadow3 is in advance scene

                //Debug.Log("shadow1: " + graphics[1].canvasRenderer.GetAlpha() + "shadow2: " + graphics[2].canvasRenderer.GetAlpha());
                break;

            case (IntroStates)6:
                WhenTimeElapsed(0.3f, 7);
                //introState = (IntroStates)7;
                break;

            case (IntroStates)7:
                graphics[4].CrossFadeAlpha(1, 0.01f, false);
                audioManager.PlayClip(0);
                introState = (IntroStates)8;
                break;

            case (IntroStates)8:
                WhenTimeElapsed(5, 9);

                break;
            case (IntroStates)9:
                graphics[0].canvasRenderer.SetAlpha(0);
                graphics[1].canvasRenderer.SetAlpha(0);
                graphics[2].canvasRenderer.SetAlpha(0);
                graphics[3].canvasRenderer.SetAlpha(0);
                graphics[5].canvasRenderer.SetAlpha(1);
                graphics[6].canvasRenderer.SetAlpha(1);
                graphics[4].CrossFadeAlpha(0, 8, false);
                introState = (IntroStates)10;
                break;

            case (IntroStates)10:
                audioManager.PlayClipsToEnd(2);

                break;
            case (IntroStates)11:
                //WhenTimeElapsed(1.5f, graphics[4], 0, 4, 9);

                break;
            case (IntroStates)12:
                //WhenTimeElapsed(1.5f, graphics[4], 0, 4, 9);

                break;

            default:
                break;
        }
        
    }
    /*
     * case IntroStates.Zero:
                WhenTimeElapsed(3,white,1,0.25f,1);
                break;
            case IntroStates.One:
                WhenGraphicFaded(white, 1, window, 0, 0.01f, 2);
                break;
            case IntroStates.Two:
                WhenGraphicFaded(window, 0, white, 0, 0.25f, 3);
                break;
    */
}
