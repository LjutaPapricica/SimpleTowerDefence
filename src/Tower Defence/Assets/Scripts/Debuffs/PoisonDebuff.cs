using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    private float tickTime;
    private float timeSinceTick;
    private float tickDamage;
    private PoisonSplash splashPrefab;

    public PoisonDebuff(float tickTime, float tickDamage, float duration, PoisonSplash splashPrefab, Mob target) : base(target, duration)
    {
        this.tickTime = tickTime;
        this.tickDamage = tickDamage;
        this.splashPrefab = splashPrefab;
    }

    public override void Update()
    {
        if (target != null)
        {
            timeSinceTick += Time.deltaTime;
            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                Splash();
            }
        }

        base.Update();
    }

    private void Splash()
    {
        PoisonSplash splash = GameObject.Instantiate(splashPrefab, target.transform.position, Quaternion.identity);
        splash.Damage = (int)tickDamage;

        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), splash.GetComponent<Collider2D>());
    }
}
