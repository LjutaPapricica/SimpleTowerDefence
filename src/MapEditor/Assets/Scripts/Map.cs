using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Map : Singleton<Map>
{
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    private GridLayoutGroup grid;
    private Sprite[] sprites;

    private readonly List<Tile> images = new List<Tile>();
    
    // Use this for initialization
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        sprites = Resources.LoadAll<Sprite>("Sprites");

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
        
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                Image image = CreateItem("Grass");
                Tile tile = image.gameObject.AddComponent<Tile>().Initialize(i, j);
                images.Add(tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Image CreateItem(string name)
    {
        GameObject imageObject = new GameObject();
        Image image = imageObject.AddComponent<Image>();
        image.sprite = sprites.First(s => s.name == name);
        imageObject.name = "Image";
        imageObject.transform.SetParent(grid.transform);
        imageObject.SetActive(true);

        return image;
    }
}
