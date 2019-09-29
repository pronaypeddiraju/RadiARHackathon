using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackingManager : MonoBehaviour
{

    [Header("Keypose Data")]
    [SerializeField]
    private MLHandKeyPose leftKeyPose;
    private MLHandKeyPose rightKeyPose;

    public Vector3 leftHandPosition = Vector3.zero;
    public Vector3 rightHandPosition = Vector3.zero;

    float leftPoseConfidence = 0.0f;
    float rightPoseConfidence = 0.0f;

    Vector3 lastFrameHandOffsetHint = Vector3.zero;
    Vector3 thisFrameHandOffsetHint = Vector3.zero;

    Vector3 lastFrameHandOffset = Vector3.zero;
    Vector3 thisFrameHandOffset = Vector3.zero;


    public static HandTrackingManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (MLHands.IsStarted)
        {
            leftKeyPose = MLHands.Left.KeyPose;
            rightKeyPose = MLHands.Right.KeyPose;

            leftHandPosition = MLHands.Left.Center;
            rightHandPosition = MLHands.Right.Center;

            leftPoseConfidence = MLHands.Left.KeyPoseConfidence;
            rightPoseConfidence = MLHands.Right.KeyPoseConfidence;

            lastFrameHandOffsetHint = thisFrameHandOffsetHint;
            thisFrameHandOffsetHint = leftHandPosition - rightHandPosition;

            lastFrameHandOffset = thisFrameHandOffset;
            thisFrameHandOffset = rightHandPosition - leftHandPosition;
        }
        else
        {
            Debug.LogError("The Magic Leap hand tracking was unable to initialize");
        }
    }

    public bool VerifyOnKeypose( MLHandKeyPose pose, bool isLeftHand)
    {
        if(isLeftHand)
        {
            if(pose == leftKeyPose)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(pose == rightKeyPose)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool VerifyKeyPoseConfidence( MLHandKeyPose pose, bool isLeftHand, float confidenceThreshold = 0.3f)
    {
        if(isLeftHand)
        {
            if(leftKeyPose == pose)
            {
                if(leftPoseConfidence > (1.0f - confidenceThreshold))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (rightKeyPose == pose)
            {
                if (rightPoseConfidence > (1.0f - confidenceThreshold))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    //Get the change in quaternion this frame
    public Quaternion GetFrameRotationObjects()
    {
        if (lastFrameHandOffset != Vector3.zero && thisFrameHandOffset != Vector3.zero)
        {
            Vector3 lastFrameNormalizedHint = lastFrameHandOffsetHint.normalized;
            Vector3 thisFrameNormalizedHint = thisFrameHandOffsetHint.normalized;

            Vector3 lastFrameNormalized = lastFrameHandOffset.normalized;
            Vector3 rightFrameNormalized = thisFrameHandOffset.normalized;

            Quaternion handRotationThisFrame = Quaternion.FromToRotation(lastFrameNormalizedHint, thisFrameNormalizedHint);
            return handRotationThisFrame;
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public MLHandKeyPose GetRightKeyPose()
    {
        return rightKeyPose;
    }

    public MLHandKeyPose GetLeftKeyPose()
    {
        return leftKeyPose;
    }

}
