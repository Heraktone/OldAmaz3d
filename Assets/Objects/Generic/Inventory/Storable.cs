using UnityEngine;
using System.Collections;

public class Storable : MonoBehaviour
{
    public GameObject storedObject;
    public bool deleteOnTrigger = true;

    public void OnInteracted(GameObject player)
    {
        var bearer = player.GetComponent<InventoryBearer>();
        bearer.AddInventory(storedObject);

        if (deleteOnTrigger)
            Destroy(this.gameObject);
    }
}
