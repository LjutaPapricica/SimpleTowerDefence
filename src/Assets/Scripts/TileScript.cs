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
    private Point gridPosition;
    private int type;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int type, Point point, Vector3 worldPoint)
    {
        this.type = type;
        gridPosition = point;
        transform.position = worldPoint;

        FloorManager.Instance.TileScripts.Add(point, this);
    }
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null && type == 0)
        {
            GameObject tower = Instantiate(GameManager.Instance.ClickedButton.Button, transform.position, Quaternion.identity);
            tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -10);
            tower.GetComponent<SpriteRenderer>().sortingOrder = gridPosition.Y;

            tower.transform.SetParent(transform);
            GameManager.Instance.BuyTower();
            Hover.Instance.Deactivate();
        }
    }
}
