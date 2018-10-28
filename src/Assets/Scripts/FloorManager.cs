using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    public float TileSize
    {
        get
        {
            return tiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Use this for initialization
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateLevel()
    {
        string[] map = LoadMap();

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int i = 0; i < map.Length; ++i)
        {
            for (int j = 0; j < map[0].Length; ++j)
            {
                int tileIndex = map[i][j] - '0';
                GameObject newTile = Instantiate(tiles[tileIndex]);
                newTile.transform.position = new Vector3(worldStart.x + TileSize * j, worldStart.y - TileSize * i, 0);
            }
        }
    }

    private string[] LoadMap()
    {
        TextAsset mapAsset = Resources.Load("Level") as TextAsset;
        return mapAsset.text.Split(Environment.NewLine.ToCharArray()).Where(i => !String.IsNullOrEmpty(i)).ToArray();
    }
}
