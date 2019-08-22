using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic Attack", menuName = "Magic Attack")]
public class MagicAttack : ScriptableObject
{
    public int baseDamage;
    public GameObject handEffect;
    public GameObject projectile;

    public float projectileSpeed;

    public float projectileDrawDelay;
    public float attackDelay;

    public Sprite icon;
}
