using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    protected Mob target;

    public Debuff(Mob target)
    {
        this.target = target;
    }

    public virtual void Update()
    {
        
    }
}
