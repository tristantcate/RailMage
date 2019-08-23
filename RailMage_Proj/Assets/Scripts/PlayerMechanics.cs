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
        if (GameManager.instance.UIClicked(cam.ScreenToViewportPoint(Input.mousePosition))) yield break;
        //Debug.Log("ui break: " + GameManager.instance.uiClicked);
        if (GameManager.instance.selectedAttackBtn.onCooldown) yield break;



        Magic selectedMagic = GameManager.instance.selectedAttackBtn.heldMagic;

        playerAC.SetBool("Attacking", true);
        playerRB.velocity = Vector2.zero;

        Destroy(Instantiate(selectedMagic.handEffect, shootFromPos), 1f);

        Vector3 pointDirection = (cam.ScreenToWorldPoint(Input.mousePosition) - shootFromPos.position);
        Vector3 rotDirection = pointDirection.normalized;
        pointDirection.z = 0;
        pointDirection = pointDirection.normalized;

        yield return new WaitForSeconds(selectedMagic.drawDelay);

        GameObject projectile = Instantiate(selectedMagic.projectile, shootFromPos);
        projectile.GetComponent<Rigidbody2D>().velocity = pointDirection * selectedMagic.projectileSpeed;
        projectile.transform.rotation = Quaternion.LookRotation(rotDirection, Vector3.forward);

        yield return new WaitForSeconds(selectedMagic.cooldown);

        playerRB.velocity = Vector2.down * playerSpeed;
        playerAC.SetBool("Attacking", false);
    }
}
