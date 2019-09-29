using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

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
    public GameObject humanToSpawnUnlit;
    public GameObject referenceCube;

    public GameObject spawnedHuman;
    public GameObject spawnedHumanUnlit;
    public Vector3 posOffset;
    //bool hasHumanSpawned = false;

    [Header("Brain Animation")]
    public Animator brainAnimator;
    public Animator brainAnimatorUnlit;
    public bool intiateAnimation = false;

    [Header("Media Player Controls")]
    public GameObject mediaPlayerParent;
    public Vector3 offsetVector;

    [Header("UI References")]
    public GameObject gesturesAnim;
    public GameObject swipeAnim;
    public Text UIInstructionText;

    public GameObject mainCamera;

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
                    if(!gesturesAnim.activeSelf)
                    {
                        gesturesAnim.SetActive(true);
                        UIInstructionText.text = "With your left hand extended, please make a fist then open your hand";
                    }

                    CheckHasSpawnedHuman();
                }
                break;
            case 2:
                {
                    if (!swipeAnim.activeSelf)
                    {
                        swipeAnim.SetActive(true);
                        UIInstructionText.text = "Please swipe on the touchpad of your Magic Leap controller";
                    }

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
        if (leftHandPose == MLHandKeyPose.Fist)
        {
            if (!hasMadeFist)
            {
                hasMadeFist = true;
            }
        }

        if (leftHandPose == MLHandKeyPose.OpenHand && hasMadeFist)
        {
            hasMadeFist = false;
            InitiateSpawnSequence();
        }
    }

    public void CheckHasFocusBrain()
    {
        Debug.Log("Brain Split Animation Initiated");

        MLInputController mlInputController = MLInput.GetController(0);
        MLInputControllerTouchpadGesture gesture = mlInputController.TouchpadGesture;
        MLInputControllerTouchpadGestureType gestureType = gesture.Type;

        if(gestureType == MLInputControllerTouchpadGestureType.Swipe)
        {
            brainAnimator.SetBool("SplitBrain", true);
            brainAnimatorUnlit.SetBool("SplitBrain", true);

            swipeAnim.SetActive(false);
            UIInstructionText.text = "";

            AudioManager._instance.PlayAudio(0);

            gameSequenceInt++;
        }
    }

    public void SetupMediaPlayer()
    {
        if (mediaPlayerParent == null)
        {
            Debug.Log("Looking for mediaPlayer in scene");
            mediaPlayerParent = GameObject.FindGameObjectWithTag("MediaPlayer");
        }

        mediaPlayerParent.SetActive(true);
        AudioManager._instance.PlayAudio(2);
        //mediaPlayerParent.transform.LookAt(mainCamera.transform);
        //mediaPlayerParent.transform.position = AppSequenceHandler.instance.spawnedHuman.transform.position + offsetVector;
        //mediaPlayerParent.transform.rotation = Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, 180.0f);
    }

    public void DisableMediaPlayer()
    {
        if (mediaPlayerParent == null)
        {
            Debug.Log("Looking for mediaPlayer in scene");
            mediaPlayerParent = GameObject.FindGameObjectWithTag("MediaPlayer");
        }

        mediaPlayerParent.SetActive(false);
        AudioManager._instance.PlayAudio(2);
    }

    public void CheckHasTextHighlight()
    {

    }

    public void InitiateSpawnSequence()
    {
        Vector3 camForward = mainCamera.transform.forward;

        gameSequenceInt++;
        Debug.Log("Spawn Initiated");
        spawnedHuman = GameObject.Instantiate(humanToSpawn);
        spawnedHumanUnlit = GameObject.Instantiate(humanToSpawnUnlit);

        spawnedHuman.transform.position = mainCamera.transform.position + camForward * 2.0f + posOffset;
        spawnedHuman.transform.rotation = Quaternion.RotateTowards(spawnedHuman.transform.rotation, mainCamera.transform.rotation, 360.0f);
        Vector3 eulerRotation = spawnedHuman.transform.rotation.eulerAngles;
        eulerRotation.z = 0.0f;
        eulerRotation.x = 0.0f;
        spawnedHuman.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, eulerRotation.z);
        spawnedHuman.transform.Rotate(new Vector3(0,180,0));

        spawnedHumanUnlit.transform.position = spawnedHuman.transform.position;
        spawnedHumanUnlit.transform.rotation = spawnedHuman.transform.rotation;
        spawnedHumanUnlit.transform.localScale = spawnedHuman.transform.localScale;
        //spawnedHuman.transform.position = referenceCube.transform.position + posOffset;

        brainAnimator = spawnedHuman.GetComponent<Animator>();
        brainAnimatorUnlit = spawnedHumanUnlit.GetComponent<Animator>();

        AudioManager._instance.PlayAudio(4);

        spawnedHuman.SetActive(true);
        spawnedHumanUnlit.SetActive(true);

        gesturesAnim.SetActive(false);
        UIInstructionText.text = "";
    }
}
