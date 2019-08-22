using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    MagicAttack heldMagicAttack;
    Image buttonIcon;
    Button button;

    void Awake()
    {
        buttonIcon = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void SetThisMagicAttack(MagicAttack toAtk)
    {
        heldMagicAttack = toAtk;
        buttonIcon.sprite = toAtk.icon;
        button.onClick.AddListener(() => SelectAtkOnClick());
    }

    public void SelectAtkOnClick()
    {
        PlayerMechanics.currentlySelectedAttack = heldMagicAttack;
        GameManager.instance.AttackBtnClicked(this);
    }
}
