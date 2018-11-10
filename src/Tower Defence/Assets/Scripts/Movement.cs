using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Point GridPosition { get; set; }
    public Vector2 WorldPosition { get; set; }
}

public class Movement : MonoBehaviour
{
    public Stack<Node> GeneratePath()
    {
        var tiles = FloorManager.Instance.TileScripts;

        List<TileScript> path = new List<TileScript>
        {
            tiles[new Point(0, 1)],
            tiles[new Point(1, 1)], tiles[new Point(2, 1)], tiles[new Point(3, 1)], tiles[new Point(4, 1)], tiles[new Point(5, 1)],
            tiles[new Point(5, 2)],
            tiles[new Point(5, 3)], tiles[new Point(4, 3)],tiles[new Point(3, 3)],tiles[new Point(2, 3)],tiles[new Point(1, 3)],
            tiles[new Point(0, 3)],
            tiles[new Point(0, 4)],
            tiles[new Point(0, 5)], tiles[new Point(1, 5)],tiles[new Point(2, 5)],tiles[new Point(3, 5)],tiles[new Point(4, 5)],
            tiles[new Point(5, 5)],
            tiles[new Point(5, 6)], tiles[new Point(5, 7)],
            tiles[new Point(4, 7)],tiles[new Point(3, 7)],tiles[new Point(2, 7)],tiles[new Point(1, 7)],
            tiles[new Point(0, 7)],
            tiles[new Point(0, 8)],
            tiles[new Point(0, 9)], tiles[new Point(1, 9)],tiles[new Point(2, 9)],tiles[new Point(3, 9)],tiles[new Point(4, 9)],
            tiles[new Point(5, 9)],
            tiles[new Point(5, 10)],tiles[new Point(5, 11)], tiles[new Point(5, 12)],
            tiles[new Point(4, 12)],tiles[new Point(3, 12)],tiles[new Point(2, 12)],tiles[new Point(1, 12)],
            tiles[new Point(0, 12)]
        };

        return new Stack<Node>(path.Select(t => new Node
        {
            GridPosition = t.GridPosition,
            WorldPosition = t.transform.position
        }));
    }
}
