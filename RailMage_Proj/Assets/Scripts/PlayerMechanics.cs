using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{

    [Header("Player Components")]
    [SerializeField] Camera cam;
    [SerializeField] Transform shootFromPos;
    Rigidbody2D playerRB;
    [SerializeField] Animator playerAC;

    [Header("Player Variables")]
    [SerializeField] float playerSpeed;

    [Header("Moves")]
    [SerializeField] MagicAttack[] magicAttacks;
    int currentlySelectedAttack = 0;
    bool currentlyShooting = false;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
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

        MagicAttack currentMagicAttack = magicAttacks[currentlySelectedAttack];

        playerAC.SetBool("Attacking", true);
        playerRB.velocity = Vector2.zero;

        Vector3 pointDirection = (cam.ScreenToWorldPoint(Input.mousePosition) - shootFromPos.position);
        Vector3 rotDirection = pointDirection.normalized;
        pointDirection.z = 0;
        pointDirection = pointDirection.normalized;
        Debug.Log(pointDirection);

        yield return new WaitForSeconds(currentMagicAttack.projectileDrawDelay);

        GameObject projectile = Instantiate(currentMagicAttack.projectile, shootFromPos);
        projectile.GetComponent<Rigidbody2D>().velocity = pointDirection * currentMagicAttack.projectileSpeed;
        projectile.transform.rotation = Quaternion.LookRotation(rotDirection, Vector3.forward);

        yield return new WaitForSeconds(currentMagicAttack.attackDelay);


        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Attacking", false);
        currentlyShooting = false;

    }
}
