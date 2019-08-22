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
    public MagicAttack[] magicAttacks;
    public static MagicAttack currentlySelectedAttack;
    bool currentlyShooting = false;

    [SerializeField] GameObject hitEffect;
    public static GameObject globalHitEffect;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        globalHitEffect = hitEffect;
        currentlySelectedAttack = magicAttacks[0];
    }

    void Start()
    {
        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Moving", true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !currentlyShooting) ShootProjectile();
    }

    void ShootProjectile() => StartCoroutine(ShootingProjectile());
    

    IEnumerator ShootingProjectile()
    {
        currentlyShooting = true;

        playerAC.SetBool("Attacking", true);
        playerRB.velocity = Vector2.zero;

        Destroy(Instantiate(currentlySelectedAttack.handEffect, shootFromPos), 1f);

        Vector3 pointDirection = (cam.ScreenToWorldPoint(Input.mousePosition) - shootFromPos.position);
        Vector3 rotDirection = pointDirection.normalized;
        pointDirection.z = 0;
        pointDirection = pointDirection.normalized;
        Debug.Log(pointDirection);

        yield return new WaitForSeconds(currentlySelectedAttack.projectileDrawDelay);

        GameObject projectile = Instantiate(currentlySelectedAttack.projectile, shootFromPos);
        projectile.GetComponent<Rigidbody2D>().velocity = pointDirection * currentlySelectedAttack.projectileSpeed;
        projectile.transform.rotation = Quaternion.LookRotation(rotDirection, Vector3.forward);

        yield return new WaitForSeconds(currentlySelectedAttack.attackDelay);

        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Attacking", false);
        currentlyShooting = false;

    }
}
