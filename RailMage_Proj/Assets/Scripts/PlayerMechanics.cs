using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Camera cam;
    [SerializeField] Animator playerAC;
    [SerializeField] Transform shootFromPos;
    Rigidbody2D playerRB;

    [Header("Player Variables")]
    [SerializeField] float playerSpeed;

    [Header("Moves")]
    public Magic[] magicAttacks;

    [SerializeField] GameObject hitEffect;
    public static GameObject globalHitEffect;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        globalHitEffect = hitEffect;
    }

    void Start()
    {
        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Moving", true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ShootProjectile();
    }

    void ShootProjectile() => StartCoroutine(CastMagic());
    

    IEnumerator CastMagic()
    {
        if (GameManager.instance.UIClicked(Input.mousePosition)) yield break;
        if (GameManager.instance.selectedAttackBtn.onCooldown) yield break;


        Magic selectedMagic = GameManager.instance.selectedAttackBtn.heldMagic;
        GameManager.instance.selectedAttackBtn.AtkUsed(selectedMagic.cooldown);
        StartCoroutine(PausePlayerWalk(selectedMagic.standstillTime));

        Destroy(Instantiate(selectedMagic.handEffect, shootFromPos), 1f);
        yield return new WaitForSeconds(selectedMagic.drawDelay);

        GameObject projectile = Instantiate(selectedMagic.projectile, shootFromPos);
        projectile.GetComponent<Projectile>().OnSpawned(selectedMagic);

        yield return new WaitForSeconds(selectedMagic.cooldown);
    }

    IEnumerator PausePlayerWalk(float pauseTime)
    {
        playerRB.velocity = Vector2.zero;
        playerAC.SetBool("Attacking", true);
        yield return new WaitForSeconds(pauseTime);
        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Attacking", false);
    }
}
