using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

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
        float size = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(size * j, size * i, 0);
            }
        }
    }
}
