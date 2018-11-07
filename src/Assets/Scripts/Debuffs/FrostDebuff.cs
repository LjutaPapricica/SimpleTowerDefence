using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostDebuff : Debuff
{
    private float maxSpeed;
    private float slowingFactor;
    private bool isApplied;

    public FrostDebuff(float slowingFactor, float duration, Mob target) : base(target, duration)
    {
        maxSpeed = target.Speed;
        this.slowingFactor = slowingFactor;
    }

    public override void Update()
    {
        if (target != null)
        {
            if (!isApplied)
            {
                isApplied = true;
                target.Speed = (maxSpeed * slowingFactor) / 100;
            }
        }
        base.Update();
    }

    public override void Remove()
    {
        target.Speed = maxSpeed;
        base.Remove();
    }
}
