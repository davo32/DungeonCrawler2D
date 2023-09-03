using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_UnitLogic : MonoBehaviour
{
    public List<GameObject> MoveToTiles = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        if (GetComponent<UnitInfo>().UnitOwnership == UnitInfo.Owner.World)
        {
            StartCoroutine(AIBareBones());
        }
    }

    private IEnumerator AIBareBones()
    {
        while (true)
        {
            if (GetComponent<UnitInfo>().CanMove)
            {
                MoveAIunit();
            }
            yield return new WaitForSeconds(Random.Range(0, 2));
        }
    }

    public void MoveAIunit()
    {
        GameManager.Instance.AIActiveUnit = gameObject;
        GameManager.Instance.ShowAvailableMoves(MoveToTiles);

        GameObject Tile = MoveToTiles[Random.Range(0, MoveToTiles.Count - 1)];
        Tile.GetComponent<TileInfo>().MoveUnit();
        Tile.GetComponent<TileInfo>().OccupiedUnit = gameObject;
        Tile.GetComponent<TileInfo>().Occupied = true;
        MoveToTiles.Clear();
    }

    public void BattlePlayer()
    {
        GameManager.Instance.AIActiveUnit = gameObject;
        GameManager.Instance.ShowAvailableMoves(MoveToTiles);

        for (int i = 0; i < MoveToTiles.Count; i++)
        {
            if (MoveToTiles[i].GetComponent<TileInfo>().Occupied)
            {
                if (MoveToTiles[i].GetComponent<TileInfo>().OccupiedUnit.GetComponent<UnitInfo>().UType == UnitInfo.UnitType.Player)
                {
                    GameObject Player = MoveToTiles[i].GetComponent<TileInfo>().OccupiedUnit;
                    Unit_BattlePanel BP = Player.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>();

                    BP.UnitStatus = (Unit_BattlePanel.Status)Random.Range(1, 3);

                    if (BP.UnitStatus == Unit_BattlePanel.Status.Attacked)
                    {
                        if (Player.GetComponent<UnitInfo>().UnitHealthCurr > 0)
                        {
                            Player.GetComponent<UnitInfo>().UnitHealthCurr -= GetComponent<UnitInfo>().UnitAttackCurr;
                            SetInteraction.Instance.ActivateInteractionText($"Player Takes {GetComponent<UnitInfo>().UnitAttackCurr} DMG", Color.red, 2);
                            break;
                        }

                        if (Player.GetComponent<UnitInfo>().UnitHealthCurr <= 0)
                        {
                            SetInteraction.Instance.ActivateInteractionText($"Player Killed", Color.red, 2);
                            Player.GetComponent<UnitInfo>().UnitHealthCurr = 0;
                        }
                    }
                    else if (BP.UnitStatus == Unit_BattlePanel.Status.Defending)
                    {
                        SetInteraction.Instance.ActivateInteractionText($"Player Defended", Color.blue, 2);
                    }
                    BP.ShowStatus(2);
                    break;
                }
                else
                {
                    if (i == MoveToTiles.Count)
                    {
                        break;
                    }
                }
            }
        }
    }
}