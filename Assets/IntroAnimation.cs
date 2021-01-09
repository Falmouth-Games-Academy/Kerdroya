using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour
{
    public enum IntroStates
    {
        Zero,
        One,//state description
        Two,
        Three,
        Four
    };
    public IntroStates introState;

    //State Zero
    //State One
    public Image white;
    //State Two
    //State Three
    //State Four

    // Start is called before the first frame update
    void Start()
    {
        //introState = IntroStates.Zero;
    }

    private void StateZero()
    {
        //Placeholder state at the start
        introState = IntroStates.One;
    }

    private void ScreenFlashWhite()
    {
        bool flashed = false;

        

        /*
        if (white != null)
        {
            if (white.color.a < 1 && flashed == false)
            {

                //white.color = new Color(1, 1, 1, Time.deltaTime * flashSpeed);
            }
        }
        */
    }

    void Test()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        white.CrossFadeAlpha(0, 2.0f, false);

        /*
        switch (introState)
        {
            case IntroStates.Zero:
                StateZero();
                break;
            case IntroStates.One:
                //ScreenFlashWhite();
                break;
            case IntroStates.Two:
                break;
            case IntroStates.Three:
                break;
            case IntroStates.Four:
                break;
            default:
                break;
        }
        */
    }
}
