using UnityEngine;
using System.Collections;


public class SpawnableInventoryItem : InventoryItem
{
    public GameObject spawnedObject;
    public bool placeObject = true;
    public float placeDistance = 2f;

    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    override public bool Activate()
    {
        Debug.Log("Activated", this);
        
        var spawned = Instantiate(spawnedObject);

        if (placeObject)
        {
            spawned.transform.position = 
                mainCamera.transform.position +
                mainCamera.transform.TransformDirection(new Vector3(0, 0, placeDistance));
        }

        return true;
    }
}