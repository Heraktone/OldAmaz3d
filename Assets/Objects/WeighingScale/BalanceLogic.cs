using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BalanceLogic : MonoBehaviour
{
    public int weightOnScale;

    void OnTriggerEnter(Collider other)
    {
        HasWeight hw = other.GetComponent<HasWeight>();
        if (hw != null)
        {
            other.transform.parent = transform;
            weightOnScale += hw.weight;
        }
    }

    void OnTriggerExit(Collider other)
    {
        HasWeight hw = other.GetComponent<HasWeight>();
        if (hw != null)
        {
            other.transform.parent = null;
            weightOnScale -= hw.weight;
        }
    }

    public void Reset()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        weightOnScale = 0;
    }
}
