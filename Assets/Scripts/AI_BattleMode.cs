using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BattleMode : MonoBehaviour
{
    public static AI_BattleMode Instance { private set; get; }

    public List<GameObject> ActionCards = new List<GameObject>();

    public GameObject E_ActiveCardOBJ;
    public ActionCardLogic E_ActiveCard;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChooseCard()
    {
        int rand = Random.Range(0, ActionCards.Count);
        E_ActiveCardOBJ = ActionCards[rand];
        E_ActiveCard = ActionCards[rand].GetComponent<ActionCardScript>()._ActionCard;
        E_ActiveCardOBJ.GetComponent<ActionCardScript>().FlipFrontCard();
        
    }

    public void ResetCards()
    {
        E_ActiveCardOBJ.GetComponent<ActionCardScript>().FlipCardBack();
       
    }
}
