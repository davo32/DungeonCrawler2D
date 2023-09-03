using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ActionCardScript : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    public ActionCardLogic _ActionCard;

    public GameObject Front;
    public TextMeshProUGUI ActionNameText;
    public GameObject StatPanel;
    public TextMeshProUGUI StatText;

    public GameObject Back;

    private GameObject[] P1Cards;

    public void Awake()
    {
        Front.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _ActionCard.Icon;
        ActionNameText.text = _ActionCard.ActionName;
        StatText.text = _ActionCard.StatAmount.ToString();

        if (_ActionCard.EffectType == ActionCardLogic.EType.Damage)
        {
            StatPanel.SetActive(true);
            StatPanel.GetComponent<Image>().color = Color.red;
            _ActionCard.EffectSpeed = Random.Range(0, 11);
        }
        else if (_ActionCard.EffectType == ActionCardLogic.EType.Defend)
        {
            StatPanel.SetActive(true);
            StatPanel.GetComponent<Image>().color = Color.blue;
            _ActionCard.EffectSpeed = Random.Range(0, 11);
        }
        else if (_ActionCard.EffectType == ActionCardLogic.EType.Utility)
        {
            StatPanel.SetActive(false);
            StatPanel.GetComponent<Image>().color = Color.green;
            _ActionCard.EffectSpeed = Random.Range(0, 11);
        }

        P1Cards = GameObject.FindGameObjectsWithTag("P1_AC");
    }

    public void FlipFrontCard()
    {
        Front.SetActive(true);
        Back.SetActive(false);

        if (gameObject.CompareTag("P1_AC"))
        {
            for (int i = 0; i < P1Cards.Length; i++)
            {
                P1Cards[i].GetComponent<Button>().interactable = false;
            }
            BattleSceneManager.Instance.P_ActionCard = _ActionCard;
            AI_BattleMode.Instance.ChooseCard();
            SetStatus();

            //card effect here


            Invoke("ResetCard", 5f);
        }
    }

    public void FlipCardBack()
    {
        Back.SetActive(true);
        Front.SetActive(false);

        if (gameObject.CompareTag("P1_AC"))
        {
            for (int i = 0; i < P1Cards.Length; i++)
            {
                P1Cards[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ResetCard()
    {

        Back.SetActive(true);
        Front.SetActive(false);

        if (gameObject.CompareTag("P1_AC"))
        {
            for (int i = 0; i < P1Cards.Length; i++)
            {
                P1Cards[i].GetComponent<Button>().interactable = true;
            }
        }

        AI_BattleMode.Instance.ResetCards();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().interactable == true)
        {
            if (_ActionCard.EffectType == ActionCardLogic.EType.Damage)
            {
                Back.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
            else if (_ActionCard.EffectType == ActionCardLogic.EType.Defend)
            {
                Back.transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            }
            else if (_ActionCard.EffectType == ActionCardLogic.EType.Utility)
            {
                Back.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Back.transform.GetChild(0).GetComponent<Image>().color = Color.black;
    }

    public void SetStatus()
    {
        if (AI_BattleMode.Instance.E_ActiveCard.EffectType == BattleSceneManager.Instance.P_ActionCard.EffectType)
        {
            if (BattleSceneManager.Instance.P_ActionCard.EffectType == ActionCardLogic.EType.Damage)
            {
                BattleSceneManager.Instance.E_Health -= BattleSceneManager.Instance.P_ActionCard.StatAmount;
                BattleSceneManager.Instance.P_Health -= AI_BattleMode.Instance.E_ActiveCard.StatAmount;
            }
            else if(BattleSceneManager.Instance.P_ActionCard.EffectType == ActionCardLogic.EType.Defend)
            {
                //do nothing
            }
        }
        else if(AI_BattleMode.Instance.E_ActiveCard.EffectType != BattleSceneManager.Instance.P_ActionCard.EffectType)
        {
            if (BattleSceneManager.Instance.P_ActionCard.EffectType == ActionCardLogic.EType.Damage && AI_BattleMode.Instance.E_ActiveCard.EffectType == ActionCardLogic.EType.Defend)
            {
                if (AI_BattleMode.Instance.E_ActiveCard.StatAmount >= BattleSceneManager.Instance.P_ActionCard.StatAmount || BattleSceneManager.Instance.P_ActionCard.StatAmount >= AI_BattleMode.Instance.E_ActiveCard.StatAmount)
                {
                    //block player attack
                }
                else if (AI_BattleMode.Instance.E_ActiveCard.StatAmount < BattleSceneManager.Instance.P_ActionCard.StatAmount)
                {
                    BattleSceneManager.Instance.E_Health -= BattleSceneManager.Instance.P_ActionCard.StatAmount;
                }
                else if (BattleSceneManager.Instance.P_ActionCard.StatAmount < AI_BattleMode.Instance.E_ActiveCard.StatAmount)
                {
                    BattleSceneManager.Instance.P_Health -= AI_BattleMode.Instance.E_ActiveCard.StatAmount;
                }
            }
            else if (BattleSceneManager.Instance.P_ActionCard.EffectType == ActionCardLogic.EType.Damage && AI_BattleMode.Instance.E_ActiveCard.EffectType == ActionCardLogic.EType.Utility)
            {
                //do utility effect
                BattleSceneManager.Instance.E_Health -= BattleSceneManager.Instance.P_ActionCard.StatAmount;

            }
            else if (BattleSceneManager.Instance.P_ActionCard.EffectType == ActionCardLogic.EType.Utility && AI_BattleMode.Instance.E_ActiveCard.EffectType == ActionCardLogic.EType.Damage)
            {
                //do utility effect
                BattleSceneManager.Instance.P_Health -= AI_BattleMode.Instance.E_ActiveCard.StatAmount;
            }
        }

        BattleSceneManager.Instance.E_HealthText.text = BattleSceneManager.Instance.E_Health.ToString();
        BattleSceneManager.Instance.P_HealthText.text = BattleSceneManager.Instance.P_Health.ToString();
    }
}