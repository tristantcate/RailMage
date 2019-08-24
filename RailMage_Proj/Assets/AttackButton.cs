using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]public Magic heldMagic;
    Image buttonIcon;
    Button button;

    #region Cooldown Variables

    [HideInInspector]public bool onCooldown; //Other scripts can check this to see if the ability is on cooldown
    WaitForSeconds cooldownStep;
    float cooldownStepTime = 0.02f;
    [SerializeField]Image cooldownImg;

    #endregion

    void Awake()
    {
        buttonIcon = GetComponent<Image>();
        button = GetComponent<Button>();

        cooldownStep = new WaitForSeconds(cooldownStepTime);
        cooldownImg.fillAmount = 0;
    }

    public void SetThisMagicAttack(Magic toAtk)
    {
        heldMagic = toAtk;
        buttonIcon.sprite = toAtk.icon;
    }

    public void SelectAtkOnClick()=> GameManager.instance.AttackBtnClicked(this);

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectAtkOnClick();
    }

    public void AtkUsed(float cooldownTime) => StartCoroutine(ButtonCooldown(cooldownTime));
    IEnumerator ButtonCooldown(float cooldownTime)
    {
        onCooldown = true;                     // Makes this ability unusable
        button.onClick.RemoveAllListeners();   // Makes this ability unselectable

        for(float i = 0; i < cooldownTime; i += cooldownStepTime)
        {
            yield return cooldownStep;
            cooldownImg.fillAmount = 1 - i / cooldownTime;
        }

        cooldownImg.fillAmount = 0;

        onCooldown = false;
        button.onClick.AddListener(() => SelectAtkOnClick());
    }

    
}
