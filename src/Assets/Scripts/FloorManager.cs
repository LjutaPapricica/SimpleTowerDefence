using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FloorManager : Singleton<FloorManager>
{
    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private GameObject startObject;
    [SerializeField]
    private GameObject endObject;

    private Point start;
    private Point finish;

    public Dictionary<Point, TileScript> TileScripts;
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
        TileScripts = new Dictionary<Point, TileScript>();
        string[] map = LoadMap();

        Vector3 maxTile = Vector3.zero;
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int i = 0; i < map.Length; ++i)
        {
            for (int j = 0; j < map[0].Length; ++j)
            {
                int tileIndex = map[i][j] - '0';

                TileScript newTile = Instantiate(tiles[tileIndex]).GetComponent<TileScript>();
                newTile.Setup(new Point(j, i), new Vector3(worldStart.x + TileSize * j, worldStart.y - TileSize * i, 0));
            }
        }

        maxTile = TileScripts[new Point(map[0].Length - 1, map.Length - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals(map[0].Length);
    }

    private string[] LoadMap()
    {
        TextAsset mapAsset = Resources.Load("Level") as TextAsset;
        return mapAsset.text.Split(Environment.NewLine.ToCharArray()).Where(i => !String.IsNullOrEmpty(i)).ToArray();
    }

    private void SpawnPortals(int width)
    {
        start = new Point(0, 0);
        finish = new Point(width - 1, 0);

        Instantiate(startObject, TileScripts[start].transform.position, Quaternion.identity);
        Instantiate(endObject, TileScripts[finish].transform.position, Quaternion.identity);
    }
}
