using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranscriptManager : MonoBehaviour
{
    public class TranscriptSentence
    {
        public float timeStamp;
        public string originalSentence;
        public string newSentence;

        public TranscriptSentence(float ts, string os, string ns)
        {
            timeStamp = ts;
            originalSentence = os;
            newSentence = ns;
        }
    }

    string originalText;
    public Text transcriptText;
    public TextMesh elapsedTimeText;
    bool transcriptPlaying;
    float startTime;
    List<TranscriptSentence> transcriptSentenceList;
    int currentTimeStampIndex;

    // Start is called before the first frame update
    void Start()
    {
        originalText = "<color=#808080ff>A diagnosis of a brain tumor is incredibly overwhelming. We all appreciate the brain is in charge of a lot of functions in our bodies and our thinking ability. " +
            "And I’m hoping that this interactive tool will help you better understand what this particular tumor that you have will impact in terms of your function and symptoms to " +
            "expect during your radiation treatment. Fortunately this brain tumor is arising out of the lining the <color=#ffa500ff><b>dura</b></color> of the brain rather than in the brain itself and is not invading " +
            "into their brain but pushing the brain aside there does not seem to be a lot of swelling. So I’m not surprised that you presented just with headaches and no specific focal " +
            "neurological symptoms in terms of what to anticipate during the radiation is a targeted treatment. So it’s going to be targeted just around those tumor with some of the normal" +
            " brain tissue around it getting high dose radiation. With this there could be swelling if the swelling creeps back into specific regions of the brain such as the motor cortex. " +
            "You may notice some weakness and these are symptoms that you want to watch for during the treatments. We can treat your symptom medically with steroid treatment if needed.</color>";

        transcriptSentenceList = new List<TranscriptSentence>();
        transcriptSentenceList.Add(new TranscriptSentence(3.36f, "A diagnosis of a brain tumor is incredibly overwhelming.", "<color=#ffffffff>A diagnosis of a brain tumor is incredibly overwhelming.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(8.19f, "We all appreciate the brain is in charge of a lot of functions in our bodies and our thinking ability.", "<color=#ffffffff>We all appreciate the brain is in charge of a lot of functions in our bodies and our thinking ability.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(14.76f, "And I’m hoping that this interactive tool will help you better understand what this particular tumor that you have", "<color=#ffffffff>And I’m hoping that this interactive tool will help you better understand what this particular tumor that you have</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(19.49f, "will impact in terms of your function and symptoms to expect during your radiation treatment.", "<color=#ffffffff>will impact in terms of your function and symptoms to expect during your radiation treatment.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(25.68f, "Fortunately this brain tumor is arising out of the lining the <color=#ffa500ff><b>dura</b></color> of the brain rather than in the brain itself", "<color=#ffffffff>Fortunately this brain tumor is arising out of the lining the <color=#ffa500ff><b>dura</b></color> of the brain rather than in the brain itself.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(31.19f, "and is not invading into their brain but pushing the brain aside there does not seem to be a lot of swelling.", "<color=#ffffffff>and is not invading into their brain but pushing the brain aside there does not seem to be a lot of swelling.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(36.61f, "So I’m not surprised that you presented just with headaches and no specific focal neurological symptoms", "<color=#ffffffff>So I’m not surprised that you presented just with headaches and no specific focal neurological symptoms</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(41f, "in terms of what to anticipate during the radiation is a targeted treatment.", "<color=#ffffffff>in terms of what to anticipate during the radiation is a targeted treatment.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(47.03f, "So it’s going to be targeted just around those tumor with some of the normal brain tissue around it getting high dose radiation.", "<color=#ffffffff>So it’s going to be targeted just around those tumor with some of the normal brain tissue around it getting high dose radiation.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(54f, "With this there could be swelling if the swelling creeps back into specific regions of the brain such as the motor cortex.", "<color=#ffffffff>With this there could be swelling if the swelling creeps back into specific regions of the brain such as the motor cortex.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(57.8f, "You may notice some weakness and these are symptoms that you want to watch for during the treatments.", "<color=#ffffffff>You may notice some weakness and these are symptoms that you want to watch for during the treatments.</color>"));
        transcriptSentenceList.Add(new TranscriptSentence(62.17f, "We can treat your symptom medically with steroid treatment if needed.", "<color=#ffffffff>We can treat your symptom medically with steroid treatment if needed.</color>"));
        ResetTranscript();
        PlayTranscript();
    }

    // Update is called once per frame
    void Update()
    {
        if (transcriptPlaying)
        {
            if (GetElapsedTime() > transcriptSentenceList[currentTimeStampIndex].timeStamp)
            {
                currentTimeStampIndex++;
                if (currentTimeStampIndex < transcriptSentenceList.Count)
                {
                    ModifyTranscript(currentTimeStampIndex);
                }
            }
        }
        //Debug.Log(GetElapsedTime());
    }

    public void PlayTranscript()
    {
        startTime = Time.time;
        transcriptPlaying = true;
        ModifyTranscript(currentTimeStampIndex);
    }

    public void PauseTranscript()
    {
        transcriptPlaying = false;
    }
    
    public void ModifyTranscript(int index)
    {
        /*
        if (currentTimeStampIndex - 1 >= 0)
        {
            transcriptText.text = transcriptText.text.Replace(transcriptSentenceList[currentTimeStampIndex - 1].newSentence, transcriptSentenceList[currentTimeStampIndex - 1].originalSentence);
        }
        */
        transcriptText.text = transcriptText.text.Replace(transcriptSentenceList[currentTimeStampIndex].originalSentence, transcriptSentenceList[currentTimeStampIndex].newSentence);
    }

    public void ResetTranscript()
    {
        transcriptText.text = originalText;
        currentTimeStampIndex = 0;
    }

    public float GetElapsedTime()
    {
        string[] times = elapsedTimeText.text.Split(':');
        return float.Parse(times[0]) * 3600 + float.Parse(times[1]) * 60 + float.Parse(times[2]);
    }

}
