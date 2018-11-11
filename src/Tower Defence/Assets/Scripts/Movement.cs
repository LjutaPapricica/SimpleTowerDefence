using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public Point GridPosition { get; set; }
    public Vector2 WorldPosition { get; set; }

    public Node(Point gridPosition, Vector2 worldPosition)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
    }
}

public class Movement : MonoBehaviour
{
    public Stack<Node> Create(string waypointFile)
    {
        Stack<Node> path = new Stack<Node>();

        foreach (string route in waypointFile.Split(System.Environment.NewLine.ToCharArray()))
        {
            Vector3 sum = new Vector3();
            int tilesCount = 0;
            Point gridPosition = new Point(-1, -1);

            if (System.String.IsNullOrEmpty(route))
                continue;

            string[] doubles = route.Split('|');
            tilesCount = doubles.Length;

            foreach (string couple in doubles)
            {
                string[] coords = couple.Split(',');
                int x = System.Int32.Parse(coords[0]);
                int y = System.Int32.Parse(coords[1]);

                TileScript tile = FloorManager.Instance.TileScripts[new Point(x, y)];
                sum += tile.transform.position;

                if (gridPosition.X == -1)
                    gridPosition = tile.GridPosition;
            }

            path.Push(new Node(gridPosition, sum / tilesCount));
        }

        return path;
    }
}
