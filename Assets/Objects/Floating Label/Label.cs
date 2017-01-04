using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Label : MonoBehaviour {
    public GameObject target;
    public Canvas canvas;

    Camera mainCamera;
    RectTransform parentCanvas;

	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        var tf = GetComponent<RectTransform>();
        var pos = mainCamera.WorldToViewportPoint(target.transform.position);
        pos.Scale(parentCanvas.sizeDelta);
        tf.position = pos;
	}

    public Label CreateLabel(RectTransform parent, GameObject target, string key, string action)
    {
        var obj = GameObject.Instantiate(this);
        var c = obj.GetComponent<Label>();
        c.target = target;
        var texts = obj.GetComponentsInChildren<Text>();
        texts[0].text = key;
        texts[1].text = action;
        obj.transform.SetParent(parent);
        return obj.GetComponent<Label>();
    }

    public void Hide()
    {
        GameObject.Destroy(gameObject);
    }
}
