using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Projectile
{
    public override void OnSpawned(Magic magicUsed)
    {
        base.OnSpawned(magicUsed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void OnHit()
    {
        base.OnHit();

    }

}
