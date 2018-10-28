using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Point point;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Point point, Vector3 worldPoint)
    {
        this.point = point;
        transform.position = worldPoint;

        FloorManager.Instance.TileScripts.Add(point, this);
    }
}
