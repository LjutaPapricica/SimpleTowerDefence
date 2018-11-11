using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public Point GridPosition { get; set; }
    public Vector2 WorldPosition { get; set; }
}

public class Movement : MonoBehaviour
{
    public Stack<Node> Create(string waypointFile)
    {
        Stack<Node> path = new Stack<Node>();

        foreach (string route in waypointFile.Split(System.Environment.NewLine.ToCharArray()))
        {
            if (System.String.IsNullOrEmpty(route))
                continue;

            string[] coords = route.Split(',');
            int x = System.Int32.Parse(coords[0]);
            int y = System.Int32.Parse(coords[1]);

            TileScript targetTile = FloorManager.Instance.TileScripts[new Point(x, y)];

            path.Push(new Node
            {
                GridPosition = targetTile.GridPosition,
                WorldPosition = targetTile.transform.position
            });
        }

        return path;
    }
}
