﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    public override void Start()
    {
        base.Start();
        ElementType = ElementType.Fire;
    }
}
