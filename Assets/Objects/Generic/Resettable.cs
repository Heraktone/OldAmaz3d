using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class Resettable : MonoBehaviour
{
    public UnityEvent postReset;
    private List<GameObject> saved;
    private List<GameObject> active;

    void Start()
    {
        saved = new List<GameObject>();
        active = new List<GameObject>();
        foreach (Transform child in transform)
        {
            var dup = GameObject.Instantiate(child.gameObject);
            dup.SetActive(false);
            saved.Add(dup);
        }
        foreach(Transform child in GetComponentsInChildren<Transform>())
        {
            if(child != transform)
                active.Add(child.gameObject);
        }
    }

    public void Reset()
    {
        foreach (GameObject child in active)
        {
            Destroy(child);
        }

        foreach (GameObject template in saved)
        {
            var obj = Instantiate(template);
            obj.transform.SetParent(transform, worldPositionStays: false);
            obj.SetActive(true);
            active.AddRange(obj.GetComponentsInChildren<Transform>().Select(tr => tr.gameObject));
        }

        postReset.Invoke();
    }
}
