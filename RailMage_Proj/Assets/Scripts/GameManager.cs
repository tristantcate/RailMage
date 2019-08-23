using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] PlayerMechanics playerMechanics;

    void Awake() => instance = this;

    [SerializeField] AttackButton[] attackBtns;
    public AttackButton selectedAttackBtn;
    [SerializeField] GameObject attackBtnSelectedSprite;

    void Start()
    {
        attackBtns[0].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[1].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[2].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[3].SetThisMagicAttack(playerMechanics.magicAttacks[0]);

        selectedAttackBtn = attackBtns[0];
    }

    public void AttackBtnClicked(AttackButton atkBtn)
    {
        selectedAttackBtn = atkBtn;
        attackBtnSelectedSprite.transform.SetParent(atkBtn.transform);
        attackBtnSelectedSprite.transform.SetAsFirstSibling();
        attackBtnSelectedSprite.transform.localPosition = Vector2.zero;
    }

    public bool UIClicked(Vector3 clickPos)
    {


        foreach(AttackButton atkbtn in attackBtns)
        {
            RectTransform atkbutt = atkbtn.GetComponent<Image>().rectTransform;
            //float threshold = atkbutt.rect.Contains(clickPos);

            //Debug.Log(clickPos.y + " | " + threshold);

            Debug.Log(atkbutt);
        }

        return false;
    }

}
