using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IProjectileHittable
{
    public void OnProjectileHit(Projectile projectile)
    {
        Debug.Log("Obstacle Hit");
        projectile.ObstacleHit();
    }
}
