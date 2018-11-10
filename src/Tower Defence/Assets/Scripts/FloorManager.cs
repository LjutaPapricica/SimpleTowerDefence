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
    private GameObject startObject;

    [SerializeField]
    private GameObject endObject;

    [SerializeField]
    private CameraMovement cameraMovement;

    public KeyPoint StartPoint { get; set; }
    public KeyPoint EndPoint { get; set; }

    private Point start;
    private Point finish;

    public Stack<Node> FinalPath { get; set; }

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
        Level map = LoadMap();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

        Vector3 maxTile = Vector3.zero;
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        foreach (Tile tile in map.Tiles)
        {
            GameObject tileObject = new GameObject();
            tileObject.SetActive(true);

            tileObject.AddComponent<SpriteRenderer>().sprite = sprites.First(s => s.name == tile.Type);
            TileScript script = tileObject.AddComponent<TileScript>();
            Vector3 location = new Vector3(worldStart.x + TileSize * tile.Location.y + TileSize / 2, worldStart.y - TileSize * tile.Location.x - TileSize / 2, 1);
            script.Setup(0, new Point((int)tile.Location.x, (int)tile.Location.y), location);

        }

        //maxTile = TileScripts[new Point(map.Length - 1, map[0].Length - 1)].transform.position;
        //cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals((int)map.Tiles.Last().Location.x);
        GeneratePath();
    }

    private Level LoadMap()
    {
        TextAsset mapAsset = Resources.Load("Maps\\level1") as TextAsset;
        return JsonUtility.FromJson<Level>(mapAsset.text);
    }

    private void SpawnPortals(int width)
    {
        start = new Point(0, 0);
        finish = new Point(0, width - 3);

        StartPoint = Instantiate(startObject, TileScripts[start].transform.position, Quaternion.identity).GetComponent<KeyPoint>();
        EndPoint = Instantiate(endObject, TileScripts[finish].transform.position, Quaternion.identity).GetComponent<KeyPoint>();
    }

    private void GeneratePath()
    {
        FinalPath = GetComponent<Movement>().GeneratePath();
    }
}
