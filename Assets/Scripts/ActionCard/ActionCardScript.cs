using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class ActionCardScript : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    public ActionCardLogic _ActionCard;

    public GameObject Front;
    public TextMeshProUGUI ActionNameText;
    public GameObject StatPanel;
    public TextMeshProUGUI StatText;

    public GameObject Back;

    private GameObject[] P1Cards;

    [SerializeField] private AudioSource SFX;

    [SerializeField] private KeyCode input;
    public void Awake()
    {
        Front.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _ActionCard.icon;
        ActionNameText.text = _ActionCard.actionName;
        StatText.text = _ActionCard.statAmount.ToString();

        if (_ActionCard.effectType == ActionCardLogic.EType.Damage)
        {
            StatPanel.SetActive(true);
            StatPanel.GetComponent<Image>().color = Color.red;
            _ActionCard.effectSpeed = Random.Range(0, 11);
        }
        else if (_ActionCard.effectType == ActionCardLogic.EType.Defend)
        {
            StatPanel.SetActive(true);
            StatPanel.GetComponent<Image>().color = Color.blue;
            _ActionCard.effectSpeed = Random.Range(0, 11);
        }
        else if (_ActionCard.effectType == ActionCardLogic.EType.Utility)
        {
            StatPanel.SetActive(false);
            StatPanel.GetComponent<Image>().color = Color.green;
            _ActionCard.effectSpeed = Random.Range(0, 11);
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
            SFX.Play();
            //card effect here


            Invoke("ResetCard", 3f);
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
            if (_ActionCard.effectType == ActionCardLogic.EType.Damage)
            {
                Back.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
            else if (_ActionCard.effectType == ActionCardLogic.EType.Defend)
            {
                Back.transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            }
            else if (_ActionCard.effectType == ActionCardLogic.EType.Utility)
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
        //Create card references
        var playerCard = BattleSceneManager.Instance.P_ActionCard;
        var enemyCard = AI_BattleMode.Instance.E_ActiveCard;
        
        //compare CardTypes between Player and Enemy
        //Do calculations
        switch (playerCard.effectType)
        {
            case ActionCardLogic.EType.Damage:
            {
                if (enemyCard.effectType == ActionCardLogic.EType.Damage)
                {
                    if (playerCard.effectSpeed >= enemyCard.effectSpeed)
                    {
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                        enemyCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                    }
                    else
                    {
                        enemyCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                    }
                }
                else if (enemyCard.effectType == ActionCardLogic.EType.Defend)
                {
                    BattleSceneManager.Instance.E_Health -= enemyCard.ApplyEffect(playerCard.statAmount);
                }
                else if (enemyCard.effectType == ActionCardLogic.EType.Utility)
                {
                    if (playerCard.effectSpeed >= enemyCard.effectSpeed)
                    {
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                        BattleSceneManager.Instance.E_Health = 
                            enemyCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                    }
                    else
                    {
                        BattleSceneManager.Instance.E_Health = 
                            enemyCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                    }
                }
                //if both damage then both take damage
                //if player damage and enemy is utility then ???
                //if player damage and enemy defense then apply effect of enemy
                break;
            }
            case ActionCardLogic.EType.Defend:
            {
                if (enemyCard.effectType == ActionCardLogic.EType.Damage)
                {
                    BattleSceneManager.Instance.P_Health -= playerCard.ApplyEffect(enemyCard.statAmount);
                }
                else if (enemyCard.effectType == ActionCardLogic.EType.Utility)
                {
                    enemyCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                }
                break;
            }
            case ActionCardLogic.EType.Utility:
            {
                if (enemyCard.effectType == ActionCardLogic.EType.Utility)
                {
                    if (playerCard.effectSpeed >= enemyCard.effectSpeed)
                    {
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                        BattleSceneManager.Instance.P_Health =
                            playerCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                    }
                    else
                    {
                        BattleSceneManager.Instance.P_Health =
                            playerCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                    }
                }
                else if (enemyCard.effectType == ActionCardLogic.EType.Damage)
                {
                    if (enemyCard.effectSpeed >= playerCard.effectSpeed)
                    {
                        enemyCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                        BattleSceneManager.Instance.E_Health = 
                            enemyCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                    }
                    else
                    {
                        BattleSceneManager.Instance.P_Health = 
                            playerCard.ApplyEffect(BattleSceneManager.Instance.P_Health);
                        playerCard.ApplyEffect(BattleSceneManager.Instance.E_Health);
                    }
                }
                break;
            }
        }
        //Set UI variables
        BattleSceneManager.Instance.E_HealthText.text = BattleSceneManager.Instance.E_Health.ToString();
        BattleSceneManager.Instance.P_HealthText.text = BattleSceneManager.Instance.P_Health.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(input) && GetComponent<Button>().interactable)
        {
            FlipFrontCard();
        }
    }
}