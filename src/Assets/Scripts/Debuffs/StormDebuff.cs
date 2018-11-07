using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDebuff : Debuff
{
    private float maxSpeed;

    public StormDebuff(float duration, Mob target) : base(target, duration)
    {
        if (target != null)
        {
            maxSpeed = target.Speed;
            target.Speed = 0;
        }
    }

    public override void Remove()
    {
        target.Speed = maxSpeed;
        base.Remove();
    }
}
