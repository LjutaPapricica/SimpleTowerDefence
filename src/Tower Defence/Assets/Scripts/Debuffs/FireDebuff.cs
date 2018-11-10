using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : Debuff
{
    private float tickTime;
    private float timeSinceTick;
    private float tickDamage;

    public FireDebuff(float tickTime, float tickDamage, float duration, Mob target) : base(target, duration)
    {
        this.tickTime = tickTime;
        this.tickDamage = tickDamage;
    }

    public override void Update()
    {
        if (target != null)
        {
            timeSinceTick += Time.deltaTime;
            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                target.TakeDamage(tickDamage, ElementType.Fire);
            }
        }

        base.Update();
    }
}
