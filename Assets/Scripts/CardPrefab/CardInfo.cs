using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public CardObject CardInHand;

    public GameObject ImageUI;
    public TextMeshProUGUI CardNameUI;
    public TextMeshProUGUI HealthUI;
    public TextMeshProUGUI SummoningCostUI;

    public GameObject PlayerInterfaceObj;
    public GameObject PlayerOneUI;

    private void Start()
    {
        PlayerInterfaceObj = GameObject.FindGameObjectWithTag("PlayerInterface");
        PlayerOneUI = GameObject.FindGameObjectWithTag("P1 UI");
    }

    private void Update()
    {
        CardNameUI.text = CardInHand.CardName;

        ImageUI.GetComponent<Image>().sprite = CardInHand.CardIcon;

        SummoningCostUI.text = CardInHand.SummoningPoints.ToString();

        HealthUI.text = CardInHand.HealthPoints.ToString();
    }

    public void ONclick()
    {
        PlayerInterfaceObj.GetComponent<PlayerInterface>().SummonMenu.SetActive(true);
        PlayerInterfaceObj.GetComponent<PlayerInterface>().CardSelected = CardInHand;

        if (PlayerInterfaceObj.GetComponent<PlayerInterface>().CardSelected != null)
        {
            PlayerInterfaceObj.GetComponent<PlayerInterface>().SummonButton.interactable = true;
            PlayerInterfaceObj.GetComponent<PlayerInterface>().CardToHide = gameObject;
        }
    }
}