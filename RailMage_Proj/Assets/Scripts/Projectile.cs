using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Animator projectileAC;
    ParticleSystem projectileParticles;


    void Awake()
    {
        projectileAC = GetComponent<Animator>();
        if(GetComponent<ParticleSystem>()) projectileParticles = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<IProjectileHittable>()?.OnProjectileHit(this);
        Debug.Log("Hit something");
    }

    public void ObstacleHit()
    {
        projectileAC.SetTrigger("Hit");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (GetComponent<ParticleSystem>()) projectileParticles.Stop();

        Destroy(Instantiate(PlayerMechanics.globalHitEffect, transform.position, Quaternion.Euler(0,0,Random.Range(0,360))), 0.33f);

        Destroy(this.gameObject, 0.3f);
    }
}
