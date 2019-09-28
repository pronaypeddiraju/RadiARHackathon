using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

[RequireComponent(typeof(HandTrackingManager))]
public class AppSequenceHandler : MonoBehaviour
{
    public static AppSequenceHandler instance;

    public HandTrackingManager handTrackingManager;

    MLHandKeyPose leftHandPose;
    MLHandKeyPose rightHandPose;

    [SerializeField]
    private int gameSequenceInt = 1;

    [Header("Human Spawn Sequence")]
    bool hasMadeFist = false;
    public GameObject humanToSpawn;
    public GameObject referenceCube;
    //bool hasHumanSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rightHandPose = handTrackingManager.GetRightKeyPose();
        leftHandPose = handTrackingManager.GetLeftKeyPose();

        switch(gameSequenceInt)
        {
            case 1:
                {
                    CheckHasSpawnedHuman();
                }
                break;
            case 2:
                {
                    CheckHasFocusBrain();
                }
                break;
            case 3:
                {
                    CheckHasTextHighlight();
                };
                break;
        }
    }

    public void CheckHasSpawnedHuman()
    {
        if (rightHandPose == MLHandKeyPose.Fist)
        {
            if (!hasMadeFist)
            {
                hasMadeFist = true;
            }
        }

        if (rightHandPose == MLHandKeyPose.OpenHand && hasMadeFist)
        {
            hasMadeFist = false;
            InitiateSpawnSequence();
        }
    }

    public void CheckHasFocusBrain()
    {

    }

    public void CheckHasTextHighlight()
    {

    }

    public void InitiateSpawnSequence()
    {
        gameSequenceInt++;
        Debug.Log("Spawn Initiated");
        GameObject go = GameObject.Instantiate(humanToSpawn);
        go.transform.position = referenceCube.transform.position;
        go.transform.Rotate(new Vector3(0,180,0));
    }
}
