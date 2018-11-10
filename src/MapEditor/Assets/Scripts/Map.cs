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
            GameObject imageObject = new GameObject();
            imageObject.AddComponent<SpriteScript>();
            Image image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
            imageObject.name = "Sprite";
            imageObject.transform.SetParent(spritesLayout.transform);
            imageObject.SetActive(true);
        }
    }

    public Image CreateItem(string name)
    {
        GameObject imageObject = new GameObject();
        Image image = imageObject.AddComponent<Image>();
        image.sprite = sprites.First(s => s.name == name);
        imageObject.name = "Image";
        imageObject.transform.SetParent(grid.transform);
        imageObject.SetActive(true);

        image.transform.localScale = new Vector3(1, 1, 1);
        return image;
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

    public void AddToTile(TileScript tile)
    {
        if (selectedSprite != null)
            tile.AddImage(selectedSprite.GetComponent<Image>().sprite);
        else
            tile.ClearNested();

    }
}
