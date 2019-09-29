using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    public Image img;
    public float fadeTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        StartFade(true);
    }

    /// <summary>
    /// Public method to handle Fade In/Out calls from other objects in scene.
    /// </summary>
    /// <param name="fadeIn"></param>
    public void StartFade(bool fadeIn)
    {
        // fades the image out when you pass true
        StartCoroutine(FadeImage(fadeIn));
    }

    /// <summary>
    /// Coroutine to trigger Fade In/Out sequence of events.
    /// Called from the StartFade function.
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <returns></returns>
    IEnumerator FadeImage(bool fadeIn)
    {
        //Debug.Log("Called Scene fade");
        if (fadeIn)
        {
            // loop over 1 second backwards
            for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= fadeTime; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
