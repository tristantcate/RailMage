using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Animator projectileAC;
    Magic thisMagic;

    void Awake()
    {
        projectileAC = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IProjectileHittable hitComponent = col.GetComponent<IProjectileHittable>();
        if(hitComponent != null)
        {
            hitComponent.OnProjectileHit(thisMagic);
            OnHit();
        }

    }

    public virtual void OnHit()
    {
        projectileAC.SetTrigger("Hit");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(Instantiate(PlayerMechanics.globalHitEffect, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360))), 0.33f);

        Destroy(this.gameObject, 0.3f);
    }

    public virtual void OnSpawned(Magic magicUsed)
    {
        thisMagic = magicUsed;

        Vector3 pointDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Vector3 rotDirection = pointDirection.normalized;
        pointDirection.z = 0;
        pointDirection = pointDirection.normalized;

        GetComponent<Rigidbody2D>().velocity = pointDirection * magicUsed.projectileSpeed;
        transform.rotation = Quaternion.LookRotation(rotDirection, Vector3.forward);
    }
}
