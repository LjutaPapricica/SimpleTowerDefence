using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    private GridLayoutGroup grid;
    private Sprite[] sprites;

    private Image[][] images;
    
    // Use this for initialization
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        sprites = Resources.LoadAll<Sprite>("Sprites");

        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;

        images = new Image[rows][];
        for (int i = 0; i < rows; ++i)
        {
            images[i] = new Image[columns];

            for (int j = 0; j < columns; ++j)
            {
                images[i][j] = CreateItem("Grass");
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
