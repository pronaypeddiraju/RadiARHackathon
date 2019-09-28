using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectManipulator>() != null)
        {
            other.GetComponent<ObjectManipulator>().SetTargetObject(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectManipulator>() != null)
        {
            other.GetComponent<ObjectManipulator>().SetTargetObject(null);
        }
    }
}
