using UnityEngine;
using System.Collections;
using System;

public class MapExplorer : MonoBehaviour
{
    protected LevelInfo info;

    [HideInInspector]
    public bool[,] exploredAreas { private set; get; }
    public int x;
    public int y;

    protected Vector2 cellSize;

    void Start()
    {
        info = GameObject.FindWithTag("LevelInfo").GetComponent<LevelInfo>();
        exploredAreas = new bool[info.mapX, info.mapY];
        for (int x = 0; x < info.mapX; x++)
        {
            for (int y = 0; y < info.mapY; y++)
            {
                exploredAreas[x, y] = false;
            }
        }
        cellSize = new Vector2(info.transform.localScale.x / (info.mapX),
                               info.transform.localScale.z / (info.mapY));
    }

    void Update()
    {
        var position = gameObject.transform.position;
        x = (int)Math.Floor(position.x / cellSize.x);
        y = info.mapY - 1 - (int)Math.Floor(position.z / cellSize.y);
        if (0 <= x && x < info.mapX && 0 <= y && y < info.mapY)
        {
            SetExplored(x, y);
        }
    }

    public delegate void HandleMapUpdate();
    public event HandleMapUpdate mapUpdated;

    void SetExplored(int x, int y)
    {
        if (!exploredAreas[x, y])
        {
            Debug.Log("explored new area");
            exploredAreas[x, y] = true;
            if(mapUpdated != null) mapUpdated();
        }
    }
}