using UnityEngine;
using System.Collections.Generic;
using System;

public class Clicker : MonoBehaviour
{
    public KeyMgr keyManager;
    public float maxDistance = 4f;
    public Label labelTemplate;
    public RectTransform labelCanvas;

    GameObject mainCamera;

    private Label currentLabel;
    private Clickable currentTarget;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        Ray ray = mainCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(.5f, .5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Clickable obj = hit.collider.GetComponentInParent<Clickable>();
            if (obj != null && obj.enabled)
            {
                if(currentTarget != obj)
                {
                    currentTarget = obj;
                    DestroyLabel();
                    currentLabel = labelTemplate.CreateLabel(labelCanvas, obj.gameObject, KeyMgr.Action.INTERACT.ToString(), obj.label);
                }
                if (keyManager.OnKeyJustPressed(KeyMgr.Action.INTERACT))
                {
                    obj.Trigger(this.gameObject);
                }
                return;
            }
        }
        currentTarget = null;
        DestroyLabel();
    }

    void DestroyLabel()
    {
        if (currentLabel != null)
        {
            currentLabel.Hide();
        }
    }
}
