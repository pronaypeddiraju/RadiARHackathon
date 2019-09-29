using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance { get; set; }
    public Component[] audioSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        //audioSources = gameObject.GetComponents(typeof(AudioSource));
    }

    public void PlayAudio(int index)
    {
        audioSources[index].GetComponent<AudioSource>().Play();
    }
}
