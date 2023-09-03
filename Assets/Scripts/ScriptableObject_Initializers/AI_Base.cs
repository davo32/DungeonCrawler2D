using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour
{

    public GameObject playerTwoUI;

    public GameObject UnitPrefab;

    public List<CardObject> AIHand = new List<CardObject>();

    public List<GameObject> StarterTiles = new List<GameObject>();

    public List<GameObject> MoveToTiles = new List<GameObject>();

    public List<GameObject> UnitsInPlay = new List<GameObject>();

    private void Start()
    {
        playerTwoUI.GetComponent<PlayerInfo>().Playername = "Bone Dungeon - Floor 0";
        playerTwoUI.GetComponent<PlayerInfo>().PlayerNameUI.text = playerTwoUI.GetComponent<PlayerInfo>().Playername;

    }


  

    public void SummonUnit()
    {
       
            GameObject Tile = StarterTiles[Random.Range(0, StarterTiles.Count)];
            GameObject Unit = Instantiate(UnitPrefab, Tile.transform.GetChild(0).position, Quaternion.identity, Tile.transform.GetChild(0).transform);

            Unit.GetComponent<UnitInfo>().UnitOwnership = UnitInfo.Owner.PlayerTwo;

            Unit.GetComponent<UnitInfo>().CardInfo = AIHand[Random.Range(0, AIHand.Count)];
            Unit.transform.SetParent(Tile.transform.GetChild(0), true);
            //Unit.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            UnitsInPlay.Add(Unit);
            AIHand.Remove(AIHand[Random.Range(0, AIHand.Count)]);
        

        //playerTwoUI.GetComponent<PlayerInfo>().SummonPoints
    }
    /// 1st Deck and Hand Setup
    /// 2nd 
    /// 

    public void MoveUnit()
    {
        //Checks to see if there are any unit's in play that the AI owns.
        if (UnitsInPlay.Count > 0)
        {
            GameManager.Instance.PlayerActiveUnit = UnitsInPlay[Random.Range(0, UnitsInPlay.Count)];

            GameManager.Instance.ShowAvailableMoves(MoveToTiles);

            MoveToTiles[Random.Range(0, MoveToTiles.Count)].GetComponent<TileInfo>().MoveUnit();

        }
    }
}
