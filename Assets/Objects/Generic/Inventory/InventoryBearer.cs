using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryBearer : MonoBehaviour {
    List<GameObject> items;

    public delegate void TakesGameObject(GameObject obj);

    public event TakesGameObject OnNewItem;

    public InventoryBearer() : base()
    {
        items = new List<GameObject>();
    }

    internal void AddInventory(GameObject anObject)
    {
        items.Add(anObject);
        Debug.Log("Added " + anObject + " to inventory");
        OnNewItem(anObject);
    }
}

public abstract class InventoryItem : MonoBehaviour
{
    public bool deleteSelf = true;
    public bool unpauseGame = true;
    public UnityEvent OnActivated;

    public abstract bool Activate();

    public void OnClick()
    {
        if (Activate())
        {
            if(deleteSelf)
            {
                DeleteSelf();
            }
            if(unpauseGame)
            {
                Unpause();
            }
            OnActivated.Invoke();
        }
    }

    protected void DeleteSelf()
    {
        Destroy(gameObject);
    }

    protected void Unpause()
    {
        Game.g.EndPause();
    }
}
