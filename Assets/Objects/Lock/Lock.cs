using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Lock : BaseTrigger
{
    public GameObject key;
    public Transform snapPosition;
    public TriggerEvent onBadKey;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject == key)
        {
            GetComponent<Collider>().enabled = false;

            var po = Component.FindObjectOfType<PickupObject>();
            if (po != null && po.carriedObject == key)
                po.Drop();

            if(snapPosition != null) {
                key.transform.position = new Vector3();
                key.transform.rotation = new Quaternion();
                key.transform.SetParent(snapPosition, worldPositionStays: false);
                key.GetComponent<Rigidbody>().isKinematic = true;
            }

            Trigger(key);
        }
        else
        {
            onBadKey.Invoke(key);
        }
    }

    public void SetKey(GameObject key)
    {
        this.key = key;
    }
}
