using UnityEngine;
using System.Collections;

public class PlayerInventoryEnterable : BaseTrigger
{
    public void OnTriggerEnter(Collider other)
    {
        if (this.enabled && other.gameObject.GetComponent<InventoryBearer>() != null)
        {
            Trigger(other.gameObject);
        }
    }
}
