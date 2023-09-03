using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupUnitDetailsP2 : MonoBehaviour
{
    public Image UnitIcon;
    public TextMeshProUGUI UnitHPText;
    public TextMeshProUGUI UnitATKText;
    public TextMeshProUGUI UnitDEFText;

    public int ATK;
    public int HP;
    public int DEF;


    public void SetupUnitUI(CardObject Unit, UnitInfo UInfo)
    {
        Sprite UIcon = Unit.CardIcon;
        HP = UInfo.UnitHealthCurr;
        ATK = UInfo.UnitAttackCurr;
        DEF = UInfo.UnitDefenseCurr;

        UnitIcon.sprite = UIcon;

        UnitHPText.text = HP.ToString();
        UnitATKText.text = ATK.ToString();
        UnitDEFText.text = DEF.ToString();

        
        //OnMouseExit - ToggleUnitUI();
    }

    public void ToggleUnitUI(bool ShowORHideUI)
    {
        
        if (ShowORHideUI)
        {
            this.GetComponent<CanvasGroup>().alpha = 1.0f;
            this.GetComponent<CanvasGroup>().interactable = true;
        }
        else if (ShowORHideUI == false)
        {
            this.GetComponent<CanvasGroup>().alpha = 0.0f;
            this.GetComponent<CanvasGroup>().interactable = false;
        }
    }

    private void Update()
    {
       UnitHPText.text = HP.ToString();
       UnitATKText.text = ATK.ToString();
       UnitDEFText.text = DEF.ToString();
    }
}
