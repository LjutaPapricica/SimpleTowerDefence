using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoisonTower : Tower
{
    [SerializeField]
    private PoisonSplash poisonPrefab;

    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(tickTime, tickDamage, debuffDuration, poisonPrefab, target);
    }

    public override void Start()
    {
        base.Start();
        ElementType = ElementType.Poison;
    }
}
