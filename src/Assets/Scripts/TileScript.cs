using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; set; }
    public int Type { get; set; }

    private Color32 redColor = new Color32(255, 118, 118, 255);
    private Color32 greenColor = new Color32(96, 255, 92, 255);

    private SpriteRenderer spriteRenderer;

    public bool IsEmpty { get; set; }
    public bool IsSelected { get; set; }

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int type, Point point, Vector3 worldPoint)
    {
        Type = type;
        IsEmpty = true;
        GridPosition = point;
        transform.position = worldPoint;

        FloorManager.Instance.TileScripts.Add(point, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GameManager.Instance.ClickedButton != null)
            {
                if (IsEmpty && Type == 0)
                {
                    ColorTile(greenColor);

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower();
                    }
                }
                else
                    ColorTile(redColor);
            }
            else if (transform.childCount > 0 && Input.GetMouseButtonDown(0))
            {
                SpriteRenderer circleRenderer = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
                IsSelected = !IsSelected;
                circleRenderer.enabled = IsSelected;
            }
        }
    }

    private void OnMouseExit()  
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        GameObject tower = Instantiate(GameManager.Instance.ClickedButton.Button, transform.position, Quaternion.identity);
        //tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -1);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);
        tower.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        IsEmpty = false;

        GameManager.Instance.BuyTower();
    }

    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
