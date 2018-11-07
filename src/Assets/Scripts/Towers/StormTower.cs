﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    public override Debuff GetDebuff()
    {
        return new FireDebuff(tickTime, tickDamage, debuffDuration, target);
    }

    public override void Start()
    {
        base.Start();
        ElementType = ElementType.Storm;
    }
}
