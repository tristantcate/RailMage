using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic Attack", menuName = "Magic Attack")]
public class Magic : ScriptableObject
{
    public int baseDamage;
    public GameObject handEffect;
    public GameObject projectile;

    public float projectileSpeed;

    public float drawDelay;
    public float cooldown;
    public float standstillTime;

    public Sprite icon;
}
