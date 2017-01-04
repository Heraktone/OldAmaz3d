using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class TriggerEvent : UnityEvent<GameObject> { }

public abstract class BaseTrigger : MonoBehaviour {
    public TriggerEvent OnInteracted;

    public void Start() { }

    public void Trigger(GameObject source)
    {
        OnInteracted.Invoke(source);
        SendMessage("OnInteracted", source, SendMessageOptions.DontRequireReceiver);
    }
}
