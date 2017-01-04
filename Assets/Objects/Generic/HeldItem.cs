using UnityEngine;
using System.Collections;

public class HeldItem : MonoBehaviour
{
    public GameObject mainCamera;

    public Vector3 positionWhileLooking = new Vector3(0, 1.12f, .66f);
    public Vector3 rotationWhileLooking = new Vector3(35, 0, 0);
    public Vector3 positionWhileNotLooking = new Vector3(0, .88f, .66f);
    public Vector3 rotationWhileNotLooking = new Vector3(10, 0, 0);
    public AnimationCurve progression = new AnimationCurve(new Keyframe[] {
        new Keyframe(0, 0),
        new Keyframe(10, 0),
        new Keyframe(35, 1),
        new Keyframe(270, 1),
        new Keyframe(271, 0),
        new Keyframe(360, 0),
    });

    public float maxDistanceDelta = 6f;
    public float maxRadiansDelta = .2f;
    public float maxMagnitudeDelta = 2f;

    public float inputW;
    public float inputT;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
        }
        transform.position = positionWhileNotLooking;
        transform.SetParent(mainCamera.transform.parent, worldPositionStays: false);
    }

    void Update()
    {
        var t = progression.Evaluate(mainCamera.transform.localEulerAngles.x);

        var targetPosition = Vector3.Lerp(positionWhileNotLooking, positionWhileLooking, t);
        var targetRotation = Vector3.Lerp(rotationWhileNotLooking, rotationWhileLooking, t);

        transform.localPosition = targetPosition;
        transform.localEulerAngles = targetRotation;
    }
}
