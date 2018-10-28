using System.Collections;
using System.Collections.Generic;
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
        string[] map = new string[]
        {
            "1101110111001",
            "0101010101001",
            "0101010101001",
            "0101010101001",
            "0101010101001",
            "0111011101111"
        };

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
}
