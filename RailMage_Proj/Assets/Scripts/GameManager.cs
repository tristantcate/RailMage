using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] PlayerMechanics playerMechanics;

    void Awake() => instance = this;

    [SerializeField] AttackButton[] attackBtns;
    [SerializeField] GameObject attackBtnSelectedSprite;

    void Start()
    {
        attackBtns[0].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[1].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[2].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[3].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
    }

    public void AttackBtnClicked(AttackButton atkBtn)
    {
        attackBtnSelectedSprite.transform.parent = atkBtn.transform;
        attackBtnSelectedSprite.transform.localPosition = Vector2.zero;
    }

}
