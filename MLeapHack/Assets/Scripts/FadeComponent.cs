using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeComponent : MonoBehaviour
{
    private Image imageToFade;
    public float fadeTime;
    private float startFadeTime;
    private bool fadeIn = true;

    // Start is called before the first frame update
    void Start()
    {
        startFadeTime = fadeTime;
        imageToFade = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fadeTime -= Time.deltaTime;

        if(fadeTime > 0 && fadeIn)
        {
            float colorVal = 1 - (fadeTime / (startFadeTime - fadeTime));
            imageToFade.color = new Color(colorVal, colorVal, colorVal, colorVal);
        }
        else
        {
            if(fadeIn)
            {
                fadeIn = false;
                fadeTime = startFadeTime;
            }

            float colorVal = 1 - (1 - (fadeTime / (startFadeTime - fadeTime)));
            imageToFade.color = new Color(colorVal, colorVal, colorVal, colorVal);
        }
    }
}
