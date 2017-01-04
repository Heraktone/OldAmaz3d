using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    public KeyMgr keyManager;
    public Clickable dropClickablePrefab;
    public string dropLabel = "Drop";

    [HideInInspector]
    public GameObject carriedObject;

    bool justStartedCarrying = false;
    Transform mainCamera;
    private GameObject clickTarget;

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>().transform;
    }

    public void PickUp(GameObject obj)
    {
        if (carriedObject == null)
        {
            carriedObject = obj;
            justStartedCarrying = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>());
            var cl = Instantiate(dropClickablePrefab);
            clickTarget = cl.gameObject;
            cl.transform.SetParent(mainCamera, worldPositionStays: false);
            cl.label = dropLabel;
            cl.OnInteracted.AddListener(o => Drop());
            obj.SendMessage("OnPickedUp", mainCamera);
        }
    }

    public void Drop()
    {
        Destroy(clickTarget);
        Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), ignore: false);
        carriedObject.SendMessage("OnDropped");
        carriedObject = null;
    }

    void Update()
    {
        if (justStartedCarrying) justStartedCarrying = false;
    }
}
