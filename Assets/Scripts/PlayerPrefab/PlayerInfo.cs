using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    #region Player Information
    [Header("Player Information")]
    public string Playername;
    #endregion

    #region References
    [Header("References")]
    public GameObject TurnIndicator;
    public TextMeshProUGUI PlayerNameUI;
    #endregion

    //Cards in hand act as characters as well as lives
    //removal of deck feature
    //max lives would be 5 (same size as player hand limit)


    private void Start()
    {
        PlayerNameUI.text = "No Summon";
    }


}
