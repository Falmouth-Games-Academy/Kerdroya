using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vignette : MonoBehaviour
{

    private Image vignette;
    private AudioSource source;
    Color col;
    [SerializeField] private float attack = 0.1f;
    [SerializeField] private float decay = 0.4f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        vignette = GetComponent<Image>();
        col = vignette.color;
        col.a = 0;
        vignette.color = col;
    }

    public void FlashVignette()
    {
        StartCoroutine(Flash(attack, decay));
        source.Play();
    }

    IEnumerator Flash(float _attack, float _decay)
    {
        col = vignette.color;
        col.a = 0;
        vignette.color = col;

        for (float t = 0; t < 1; t += (Time.deltaTime / _attack))
        {
            col.a = t;
            vignette.color = col;
            yield return null;
        }

        for (float t = 1; t > 0; t -= (Time.deltaTime / _decay))
        {
            col.a = t;
            vignette.color = col;
            yield return null;
        }

        col.a = 0;
        vignette.color = col;
    }

}
