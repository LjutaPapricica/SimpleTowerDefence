using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    [SerializeField]
    private GridLayoutGroup spritesLayout;

    private GridLayoutGroup grid;
    private Sprite[] sprites;

    private SpriteScript selectedSprite;

    private readonly List<TileScript> images = new List<TileScript>();

    // Use this for initialization
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;

        CreateSprites();

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                Image image = CreateItem("Grass");
                TileScript tile = image.gameObject.AddComponent<TileScript>().Initialize(i, j);
                images.Add(tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedSprite.Deselect();
            selectedSprite = null;
        }
    }

    private void CreateSprites()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites");
        foreach (Sprite sprite in sprites)
        {
            GameObject spriteObject = ImageHelper.CreateImage(sprite, spritesLayout.transform, sprite.name);
            spriteObject.AddComponent<SpriteScript>();
        }
    }

    public Image CreateItem(string name)
    {
        GameObject image = ImageHelper.CreateImage(sprites.First(s => s.name == name), grid.transform, name, new Vector3(1, 1, 1));
        return image.GetComponent<Image>();
    }

    public void SelectTile(TileScript tile)
    {
        if (selectedSprite != null)
            tile.ChangeSprite(selectedSprite.GetComponent<Image>().sprite);
    }

    public void SelectSprite(SpriteScript sprite)
    {
        if (selectedSprite != null)
            selectedSprite.Deselect();

        selectedSprite = sprite;
    }

    public void ContextTile(TileScript tile)
    {
        if (selectedSprite != null)
            tile.AddImage(selectedSprite.GetComponent<Image>().sprite);
        else
            tile.ClearNested();
    }

    public void Serialize()
    {
        Level level = new Level
        {
            Tiles = images.Select(i => new Tile
            {
                Location = new Vector2(i.X, i.Y),
                Type = i.Type,
                Items = i.NestedImages.Select(n => new Item
                {
                    Type = n.name
                }).ToList()
            }).ToList()
        };

        string json = JsonUtility.ToJson(level);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "level.json"), json);
        Debug.Log(Path.Combine(Application.persistentDataPath, "level.json"));
    }
}
