using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject AIPlayer;

    public GameObject CurrentPlayer;

    public Button P1EndTurnButton;

    public enum Phases {Start,Draw,Summon,Main,End};
    public Phases CurrentPhase = Phases.Start;

    private void Start()
    {
        CurrentPlayer = Player1;
       // CurrentPlayer.GetComponent<PlayerInfo>().playerTurn = true;

        StartTurn();
    }

    public void StartTurn()
    {
        CurrentPhase = Phases.Start;

        //Updates player ppoints for any player that is not the AI
        //CurrentPlayer.GetComponent<PlayerInfo>().UpdatePlayerPoints();


        Debug.Log("CurrentPlayer SP and MP Reset");
        DrawPhase();
    }

    public void DrawPhase()
    {
        CurrentPhase = Phases.Draw;

        if (CurrentPlayer == Player1)
        {
           // for (int i = 0; i < CurrentPlayer.GetComponent<PlayerInfo>().Hand.Count; i++)
           // {
               // if (CurrentPlayer.GetComponent<PlayerInfo>().Hand[i].GetComponent<CanvasGroup>().alpha == 0.0f)
               // {
                   // CurrentPlayer.GetComponent<PlayerInfo>().Hand[i].GetComponent<CardInfo>().CardInHand = CurrentPlayer.GetComponent<PlayerInfo>().Deck[i];
                   // CurrentPlayer.GetComponent<PlayerInfo>().Hand[i].GetComponent<CanvasGroup>().alpha = 1.0f;
               // }
           // }
        }

        Debug.Log("CurrentPlayer Draws 1 Card");
        SummonPhase();
    }

    public void SummonPhase()
    {
        CurrentPhase = Phases.Summon;

        if (CurrentPlayer == Player2)
        {
            AIPlayer.GetComponent<AI_Base>().SummonUnit();
            MainPhase();
        }
        Debug.Log("CurrentPlayer is Allowed to Summon Units");
    }

    public void MainPhase()
    {
        CurrentPhase = Phases.Main;
        AIPlayer.GetComponent<AI_Base>().MoveUnit();
        EndPhase();
        Debug.Log("CurrentPlayer is Allowed to Move and attack/Defend with Units");
    }

    public void EndPhase()
    {
        CurrentPhase = Phases.End;

        Debug.Log("CurrentPlayer Switches for next turn");

        //CurrentPlayer.GetComponent<PlayerInfo>().playerTurn = false;

        if (CurrentPlayer == Player1)
        {
            CurrentPlayer = Player2;
            P1EndTurnButton.interactable = false;
        }
        else if (CurrentPlayer == Player2)
        {
            CurrentPlayer = Player1;
            P1EndTurnButton.interactable = true;
        }

        //CurrentPlayer.GetComponent<PlayerInfo>().playerTurn = true;
        StartTurn();
    }

    public void EndTurnButtonClick()
    {
        EndPhase();
    }
}
