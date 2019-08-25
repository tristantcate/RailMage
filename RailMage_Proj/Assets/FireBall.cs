using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    ParticleSystem projectileParticles;

    public override void OnSpawned(Magic magicUsed)
    {
        base.OnSpawned(magicUsed);
        projectileParticles = GetComponent<ParticleSystem>();
        Destroy(gameObject, 10);
    }

    public override void OnHit()
    {
        base.OnHit();
        projectileParticles.Stop();

    }

}
