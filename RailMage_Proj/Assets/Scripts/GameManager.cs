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

    [SerializeField]LevelSpecs currentLevelSpecs;
    [SerializeField] GameObject[] Levels;

    void Start()
    {
        attackBtns[0].SetThisMagicAttack(playerMechanics.magicAttacks[0]);
        attackBtns[1].SetThisMagicAttack(playerMechanics.magicAttacks[1]);
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
            Vector2 localMousePos = atkbutt.InverseTransformPoint(clickPos);
            if(atkbutt.rect.Contains(localMousePos)) return true;
        }

        return false;
    }

    public float LevelEndPos() => currentLevelSpecs.playerFinishPos.position.y;

    public float NormalisedDistanceToEnd()
    {
        float totalStageLength = currentLevelSpecs.playerStartPos.position.y - currentLevelSpecs.playerFinishPos.position.y;
        totalStageLength = Mathf.Abs(totalStageLength);

        return (totalStageLength - (Mathf.Abs(currentLevelSpecs.playerFinishPos.position.y - playerMechanics.transform.position.y))) / totalStageLength;

    }

}
