using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    public CardObject CardSelected;
    public GameObject CardToHide;
    public GameObject UnitPrefab;

    public GameObject PlayerOneUI;

    #region SummonMenu
    public GameObject SummonMenu;
    public Button SummonButton;

    public bool CanSummon;
    #endregion

    #region ActionMenu
    public GameObject ActionMenu;
    #endregion


    private void Start()
    {
        PlayerOneUI = GameObject.FindGameObjectWithTag("P1 UI");
    }

    #region SummonMenu
    public void OnCancelSummonMenu()
    {
        CardSelected = null;
        SummonButton.interactable = false;
        SummonMenu.SetActive(false);
    }

    public void OnSummonFromHand()
    {
        CanSummon = true;
    }
    #endregion

    #region ActionMenu
    public void OnMoveFromField()
    {
        GameManager.Instance.PlayerActiveUnit.GetComponent<UnitInfo>().CanMove = true;

        //Check tiles able to move to from here and light them up a color.
        //GameManager.Instance.ShowAvailableMoves();
    }
    #endregion
}
