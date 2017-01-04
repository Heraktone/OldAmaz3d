using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Map : MonoBehaviour
{
    [Tooltip("The sprite used to mask unexplored areas")]
    public GameObject maskSprite;
    [Tooltip("The UI object where all the unexplored area sprites will be put")]
    public RectTransform masksContainer;
    [Tooltip("The UI object containing the map image.")]
    public Image mapImage;

    [Tooltip("Automatically set to the player's")]
    public MapExplorer explorer;

    public bool updateEveryTime = false;
    public Vector2 offset = new Vector2(0, 0);
    public float divadd = 0;


    protected LevelInfo info;

    void Start()
    {
        info = GameObject.FindWithTag("LevelInfo").GetComponent<LevelInfo>();
        explorer = GameObject.FindWithTag("Player").GetComponent<MapExplorer>();
        mapImage.sprite = info.levelMap;

        explorer.mapUpdated += Redraw;

        PaintMasks();
    }

    void Redraw()
    {
        DeleteAllMasks();
        PaintMasks();
    }

    void DeleteAllMasks()
    {
        foreach (Transform ch in masksContainer.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(ch.gameObject);
        }
    }

    void PaintMasks()
    {
        var scale = new Vector3(masksContainer.sizeDelta.x / (info.mapX + divadd), masksContainer.sizeDelta.y / (info.mapY + divadd), 1);

        for (int x = 0; x <= info.mapX + 1; x++)
        {
            for (int y = 0; y <= info.mapY + 1; y++)
            {
                if (IsUnexplored(x - 1, y - 1))
                {
                    var sprite = Instantiate(maskSprite);
                    sprite.transform.localScale = new Vector3(1, 1, 1);
                    sprite.transform.position = new Vector3((x + offset.x) * scale.x, (offset.y - y) * scale.y);
                    sprite.transform.SetParent(masksContainer, worldPositionStays: false);
                }
            }
        }
    }

    bool IsUnexplored(int x, int y)
    {
        return x < 0 || y < 0 || x >= info.mapX || y >= info.mapY
            || !explorer.exploredAreas[x, y];
    }
}
