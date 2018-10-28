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

    [SerializeField]
    private CameraMovement cameraMovement;

    private Dictionary<Point, TileScript> tileScripts;
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
        tileScripts = new Dictionary<Point, TileScript>();
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

                tileScripts.Add(new Point(j, i), newTile);
            }
        }

        maxTile = tileScripts[new Point(map[0].Length - 1, map.Length - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private string[] LoadMap()
    {
        TextAsset mapAsset = Resources.Load("Level") as TextAsset;
        return mapAsset.text.Split(Environment.NewLine.ToCharArray()).Where(i => !String.IsNullOrEmpty(i)).ToArray();
    }
}
