using System;
using System.Collections;
using System.Collections.Generic;
using JoshH.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCard : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private ActionCardLogic ActionCard;
    public bool isMainCard = false;
    
    [Header("Required")]
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private Image CardImage;

    private void Start()
    {
        UpdateCard();
    }

    public void UpdateCard()
    {
        CardImage.sprite = ActionCard.Icon;
        if(isMainCard) TitleText.text = ActionCard.ActionName;
        switch (ActionCard.EffectType)
        {
            case ActionCardLogic.EType.Damage:
            {
                GetComponent<UIGradient>().CornerColorUpperRight = Color.red;
                GetComponent<UIGradient>().CornerColorLowerRight = Color.red;
                GetComponent<UIGradient>().CornerColorLowerLeft = Color.red;
                break;
            }
            case ActionCardLogic.EType.Defend:
            {
                GetComponent<UIGradient>().CornerColorUpperRight = Color.blue;
                GetComponent<UIGradient>().CornerColorLowerRight = Color.blue;
                GetComponent<UIGradient>().CornerColorLowerLeft = Color.blue;
                break;
            }
            case ActionCardLogic.EType.Utility:
            {
                GetComponent<UIGradient>().CornerColorUpperRight = Color.green;
                GetComponent<UIGradient>().CornerColorLowerRight = Color.green;
                GetComponent<UIGradient>().CornerColorLowerLeft = Color.green;
                break;
            }
            case ActionCardLogic.EType.None:
                break;
        }
    }
}
