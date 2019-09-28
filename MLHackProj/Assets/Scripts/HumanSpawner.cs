using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    public void SetObjectToSpawn(GameObject objToSpawn)
    {
        objectToSpawn = objToSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initiated Spawn");
        GameObject.Instantiate(objectToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
