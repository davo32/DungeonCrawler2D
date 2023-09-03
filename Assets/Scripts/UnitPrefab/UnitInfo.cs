using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{

    public enum UnitType { None, Player, Enemy, Item, Interactable };
    public UnitType UType = UnitType.None;

    public CardObject CardInfo;

    public string UnitName;

    public GameObject ImageUI;

    public GameObject PlayerInterfaceObj;
    public GameObject PlayerOneUI;

    public GameObject P1UnitDetails;
    public GameObject P2UnitDetails;

    public GameObject CurrTile;

    public int UnitHealthCurr;
    public int UnitAttackCurr;
    public int UnitDefenseCurr;

    public enum Owner { None, World, PlayerOne, PlayerTwo };
    public Owner UnitOwnership = Owner.None;

    public bool CanMove;
    private void Start()
    {
        PlayerInterfaceObj = GameObject.FindGameObjectWithTag("PlayerInterface");


        PlayerOneUI = GameObject.FindGameObjectWithTag("P1 UI");
        P1UnitDetails = GameObject.FindGameObjectWithTag("P1UnitDetails");
        P2UnitDetails = GameObject.FindGameObjectWithTag("P2UnitDetails");
       
        CurrTile = transform.parent.transform.parent.gameObject;
        
        if (CurrTile)
            CurrTile.GetComponent<TileInfo>().SetUnit();
        else
            Debug.Log("CurrTile not set...");

        UnitName = CardInfo.CardName;

        ImageUI.GetComponent<Image>().sprite = CardInfo.CardIcon;

        UnitHealthCurr = CardInfo.HealthPoints;
        UnitAttackCurr = CardInfo.AttackPoints;
        UnitDefenseCurr = CardInfo.DefensePoints;



        //CurrTile.GetComponent<TileInfo>().SetUnit(gameObject);
        if (UnitOwnership == Owner.PlayerOne)
        {
            P1UnitDetails.GetComponent<SetupUnitDetailsP1>().SetupUnitUI(CardInfo, this);
            P1UnitDetails.GetComponent<SetupUnitDetailsP1>().ToggleUnitUI();

            PlayerInterfaceObj.GetComponent<CanvasGroup>().alpha = 0.0f;
            PlayerInterfaceObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
            PlayerInterfaceObj.GetComponent<CanvasGroup>().interactable = false;
        }

        CanMove = true;
    }

    public void ShowActionMenu()
    {

    }



    public void ShowUnitDetails()
    {
        if (UnitOwnership == Owner.World)
        {
            P2UnitDetails.GetComponent<SetupUnitDetailsP2>().SetupUnitUI(CardInfo, this);
            P2UnitDetails.GetComponent<SetupUnitDetailsP2>().ToggleUnitUI(true);
        }

    }



    private void Update()
    {
        if (UnitHealthCurr <= 0)
        {
            Debug.Log($"{UnitName} is DEAD");
            P2UnitDetails.GetComponent<SetupUnitDetailsP2>().ToggleUnitUI(false);
            Destroy(gameObject);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            GameManager.Instance.TestKeyboardMovement("W");
            Debug.Log("W: PRESSED");
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            GameManager.Instance.TestKeyboardMovement("S");
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            GameManager.Instance.TestKeyboardMovement("D");
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            GameManager.Instance.TestKeyboardMovement("A");
        }
        else
        {
            CurrTile = transform.parent.transform.parent.gameObject;
        }

    }
    private void OnDestroy()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.transform.parent.parent.gameObject.GetComponent<TileInfo>().Occupied = false;

        if (UnitOwnership == Owner.PlayerOne)
        {
            PlayerOneUI.GetComponent<PlayerInfo>().PlayerNameUI.text = "No Summon";
            P1UnitDetails.GetComponent<SetupUnitDetailsP1>().ToggleUnitUI();

            //cards
            PlayerInterfaceObj.GetComponent<CanvasGroup>().alpha = 1.0f;
            PlayerInterfaceObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            PlayerInterfaceObj.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}
