using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IProjectileHittable
{
    #region All stats
    [Header("Stats")]
    public int maxHealth;
    int currentHealth;
    public int atkPower;


    [System.Serializable]
    public struct DamageAffector
    {
        public Magic resistingMagic;
        public float resistMultiplier; // 0 means no damage
    }

    [Header("Resistances & Weaknesses")]
    public DamageAffector[] damageAffectors;
    public Magic[] effectResistances;

    #endregion

    #region Components
    [Header("Components")]
    protected Animator enemyAC;

    WaitForSeconds healthBarDrainStep = new WaitForSeconds(0.02f);
    [SerializeField] Image barFill;
    Animator barFillAnimator;

    void Start()
    {
        enemyAC = GetComponent<Animator>();
        barFillAnimator = barFill.transform.parent.GetComponent<Animator>();

        currentHealth = maxHealth;
        UpdateBarFill();
    }

    #endregion

    public void OnProjectileHit(Magic magicUsed)
    {
        int damage = magicUsed.baseDamage;
        foreach(DamageAffector dmgAffector in damageAffectors)
            if (magicUsed == dmgAffector.resistingMagic) damage = Mathf.FloorToInt((float)damage * dmgAffector.resistMultiplier);


        Damage(damage);
    }


    private void Damage(int damage)
    {
        currentHealth -= damage;
        UpdateBarFill();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            barFillAnimator.SetBool("Dead", true);
            Die();
        }
        else SpecificHitAction();

        Debug.Log("Enemy dmaage" + damage + " | Health: " + currentHealth);
    }

    void UpdateBarFill()
    {
        barFill.fillAmount = (float)currentHealth / maxHealth;
        barFill.color = Color.Lerp(GameManager.instance.emptyColor, GameManager.instance.fullColor, barFill.fillAmount);
    }

    public virtual void SpecificHitAction() { }
    public virtual void Die() { }
}
