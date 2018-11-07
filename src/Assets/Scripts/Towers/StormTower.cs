using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    public override void Start()
    {
        base.Start();
        ElementType = ElementType.Storm;
    }
}
