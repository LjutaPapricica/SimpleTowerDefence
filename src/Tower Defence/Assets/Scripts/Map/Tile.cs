using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Tile
{
    public string Type;
    public Vector2 Location;
    public List<Item> Items;
}
