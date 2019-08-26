using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Enemy
{
    Rigidbody2D blobRB;
    [SerializeField] float walkSpeed;

    [SerializeField] float stunTime;
    WaitForSeconds stunWait;
    bool stunned;
    bool dying;

    void Awake()
    {
        blobRB = GetComponent<Rigidbody2D>();
        stunWait = new WaitForSeconds(stunTime);
    }

    void Update()
    {
        if (!stunned && !dying)
        {
            blobRB.velocity = (GameManager.instance.GetPlayerPos() - transform.position).normalized * walkSpeed;
        }
        else
        {
            blobRB.velocity = Vector2.zero;
        }
    }

    public override void SpecificHitAction()
    {
        enemyAC.SetTrigger("Hit");
        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        stunned = true;
        yield return stunWait;
        stunned = false;
    }

    public override void Die()
    {
        dying = true;
        enemyAC.SetTrigger("Dead");
        Destroy(gameObject, 1f);
    }
}
