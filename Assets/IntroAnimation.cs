using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
                WhenTimeElapsed(5, 9);//white wait

                break;
            case (IntroStates)9:
                graphics[0].canvasRenderer.SetAlpha(0);
                graphics[1].canvasRenderer.SetAlpha(0);
                graphics[2].canvasRenderer.SetAlpha(0);
                graphics[3].canvasRenderer.SetAlpha(0);
                graphics[5].canvasRenderer.SetAlpha(1);
                graphics[6].canvasRenderer.SetAlpha(1);
                graphics[4].CrossFadeAlpha(0, 6, false);
                introState = (IntroStates)10;

                break;

            case (IntroStates)10:
                audioManager.PlayClipsToEnd(3);
                WhenTimeElapsed(7, 11);//Jan Tragegal

                break;
            case (IntroStates)11:
                graphics[7].canvasRenderer.SetAlpha(1);
                graphics[8].canvasRenderer.SetAlpha(1);
                WhenTimeElapsed(5.5f, 12);

                break;
            case (IntroStates)12:
                graphics[7].canvasRenderer.SetAlpha(0);
                graphics[8].canvasRenderer.SetAlpha(0);
                WhenTimeElapsed(7, 13);//Born
                break;

            case (IntroStates)13:
                graphics[7].canvasRenderer.SetAlpha(1);
                graphics[9].canvasRenderer.SetAlpha(1);
                WhenTimeElapsed(5.5f, 14);
                break;

            case (IntroStates)14:
                graphics[7].canvasRenderer.SetAlpha(0);
                graphics[9].canvasRenderer.SetAlpha(0);
                WhenTimeElapsed(7, 15);//Died
                break;
            case (IntroStates)15:
                graphics[7].canvasRenderer.SetAlpha(1);
                graphics[10].canvasRenderer.SetAlpha(1);
                WhenTimeElapsed(5.5f, 16);
                break;
            case (IntroStates)16:
                graphics[7].canvasRenderer.SetAlpha(0);
                graphics[10].canvasRenderer.SetAlpha(0);
                WhenTimeElapsed(7, 17);//Summoned
                break;
            case (IntroStates)17:
                graphics[7].canvasRenderer.SetAlpha(1);
                graphics[11].canvasRenderer.SetAlpha(1);
                WhenTimeElapsed(5.5f, 18);
                break;
            case (IntroStates)18:
                graphics[7].canvasRenderer.SetAlpha(0);
                graphics[11].canvasRenderer.SetAlpha(0);
                WhenTimeElapsed(7, 19);//Cursed
                break;
            case (IntroStates)19:
                graphics[7].canvasRenderer.SetAlpha(1);
                graphics[12].canvasRenderer.SetAlpha(1);
                WhenTimeElapsed(5.5f, 20);
                break;
            case (IntroStates)20:
                graphics[7].canvasRenderer.SetAlpha(0);
                graphics[12].canvasRenderer.SetAlpha(0);
                if (audioManager.coRoutineEnded)
                {
                    introState = (IntroStates)21;
                }
                break;
            case (IntroStates)21:
                WhenTimeElapsed(6f, 22);

                break;
            case (IntroStates)22:
                SceneManager.LoadScene("MainMenu");

                break;

            default:
                break;
        }
        
    }

}
