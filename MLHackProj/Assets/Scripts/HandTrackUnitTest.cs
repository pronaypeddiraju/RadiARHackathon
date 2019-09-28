using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackUnitTest : MonoBehaviour
{
    public HandTrackingManager handTrackingManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handTrackingManager != null)
        {
            gameObject.transform.rotation *= handTrackingManager.GetFrameRotationObjects();
        }
    }
}
