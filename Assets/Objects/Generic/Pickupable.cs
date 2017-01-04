using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour
{
    public float idealDistance = 2;
    public float unitsPerSecond = 6;
    public Vector3 torque = new Vector3(3e-7f, 3e-7f, 3e-7f);

    void OnInteracted(GameObject source)
    {
        var player = source.GetComponent<PickupObject>();
        player.PickUp(gameObject);
    }

    bool pickedUp = false;
    bool usedGravity;
    Transform mainCamera;
    Rigidbody rb;

    void OnPickedUp(Transform cam)
    {
        print("Picking up " + gameObject);
        mainCamera = cam;
        pickedUp = true;
        rb = GetComponent<Rigidbody>(); // FIXME multiple bodies ?
        usedGravity = rb.useGravity;
        rb.useGravity = false;
        rb.AddTorque(torque);
    }

    void FixedUpdate()
    {
        if (pickedUp)
        {
            var speed = (getIdealPosition() - transform.position) * unitsPerSecond;
            rb.velocity = speed;
        }
    }

    Vector3 getIdealPosition()
    {
        return mainCamera.position
             + mainCamera.TransformDirection(new Vector3(0, 0, idealDistance));
    }

    void OnDropped()
    {
        pickedUp = false;
        var rb = GetComponent<Rigidbody>(); // FIXME multiple bodies ?
        if (rb)
        {
            rb.useGravity = usedGravity;
        }
    }
}
