using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingAnimation : MonoBehaviour
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
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
        Nineteen,
        Twenty
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
                FadeGraphicOnce(0, 1, 5);//Fade the window in
                introState = (IntroStates)1;
                break;

            case (IntroStates)1:
                WhenTimeElapsed(3, 2);
                break;

            case (IntroStates)2:
                audioManager.PlayClipsToEnd(1);
                introState = (IntroStates)3;
                break;

            case (IntroStates)3:
                WhenTimeElapsed(28, 4);
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
                WhenTimeElapsed(9, 9);

                break;
            case (IntroStates)9:
                SceneManager.LoadScene("Credits Scene");

                break;

            default:
                break;
        }

    }

}
