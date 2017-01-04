using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {
    public InventoryBearer bearer;
    public float preferredHeight = 32;
    public float preferredWidth = 32;

    public void Start()
    {
        bearer.OnNewItem += OnNewItem;
    }

    public void OnNewItem(GameObject item)
    {
        Debug.Log("Adding to view");
        var invItem = Instantiate(item);
        var el = invItem.AddComponent<LayoutElement>();
        el.preferredHeight = preferredHeight;
        el.preferredWidth = preferredWidth;
        invItem.transform.SetParent(this.transform);
    }
}
