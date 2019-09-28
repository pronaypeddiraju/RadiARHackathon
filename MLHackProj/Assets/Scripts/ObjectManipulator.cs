using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    public Transform snapTransform; //transform that the target object will snap to and follow
    GameObject targetobject; //target object to be snapped to snap transfrom and manipulated

    //Snap target object to grasp transform
    public void SnapObjectToHand()
    {
        if (targetobject != null)
        {
            targetobject.transform.SetParent(snapTransform);
        }
    }

    //Unparent object from the snap transform
    public void ReleaseObject()
    {
        targetobject.transform.parent = null;
    }

    //Set target object to be snapped to grasp transfrom and manipulated
    public void SetTargetObject(GameObject newTarget)
    {
        targetobject = newTarget;
    }

    public void ScaleObject(Vector3 newScale)
    {
        if (targetobject != null)
        {
            targetobject.transform.localScale = newScale;
        }
    }

    public void RotateObject(Vector3 newRotation)
    {
        if (targetobject != null)
        {
            targetobject.transform.eulerAngles = newRotation;
        }
    }
}
