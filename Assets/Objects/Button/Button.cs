using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    public Animator movingPart;

    public void OnInteracted(GameObject source)
    {
        Debug.Log("Been interacted with!");
        movingPart.SetTrigger("PressOnce");
    }

    public void OnUpdate()
    {
        movingPart.SetBool("isPressed", false);
    }
}
