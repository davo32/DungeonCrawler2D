using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public GameObject BattleScreen;

    public GameObject LevelUI;

    public GameObject PlayerActiveUnit;
    public GameObject AIActiveUnit;

    public List<GameObject> Tiles = new List<GameObject>();
    public List<GameObject> MTiles = new List<GameObject>();
    public GameObject ECurrTile;

    public GameObject CurrTile;
    public GameObject NxtTile;

    public GameObject UnitPrefab;

    public bool Connection;
    // public int DMG = -1;

    private void Awake()
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

    #region Movement

    public void Start()
    {
        LevelUI.GetComponent<PlayerInfo>().PlayerNameUI.text = "Babel Dungeon";
    }

    public void ShowAvailableMoves(List<GameObject> Temp)
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].name == AIActiveUnit.transform.parent.transform.parent.gameObject.name)
            {
                ECurrTile = AIActiveUnit.transform.parent.transform.parent.gameObject;
                Debug.Log("Enemy Current Tile Set to  " + i);

                //Forward
                if (i + 12 < Tiles.Count)
                {
                    if (Tiles[i + 12].GetComponent<TileInfo>().Occupied == false)
                    {
                        Tiles[i + 12].GetComponent<TileInfo>().CanMoveTo = true;
                        Temp.Add(Tiles[i + 12]);
                    }
                    else
                    {
                        break;
                    }
                }

                //Right
                if (i + 1 < Tiles.Count)
                {
                    if (Tiles[i + 1].GetComponent<TileInfo>().Occupied == false)
                    {
                        Tiles[i + 1].GetComponent<TileInfo>().CanMoveTo = true;
                        Temp.Add(Tiles[i + 1]);
                    }
                    else
                    {
                        break;
                    }
                }

                //Left
                if (i - 1 < Tiles.Count)
                {
                    if (Tiles[i - 1].GetComponent<TileInfo>().Occupied == false)
                    {
                        Tiles[i - 1].GetComponent<TileInfo>().CanMoveTo = true;
                        Temp.Add(Tiles[i - 1]);
                    }
                    else
                    {
                        break;
                    }
                }

                //Backwards
                if (i - 12 < Tiles.Count)
                {
                    if (Tiles[i - 12].GetComponent<TileInfo>().Occupied == false)
                    {
                        Tiles[i - 12].GetComponent<TileInfo>().CanMoveTo = true;
                        Temp.Add(Tiles[i - 12]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    public void TestKeyboardMovement(string Key)
    {
        Debug.Log($"KEY IS: {(KeyCode)System.Enum.Parse(typeof(KeyCode), Key)}");


        if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<TileInfo>().TransportTile)
        {
            if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<ConnectorTile>().Direction == (KeyCode)System.Enum.Parse(typeof(KeyCode), Key))
            {
                //needs something here
                Debug.Log("Transport Tile");
                PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<ConnectorTile>().ChangeScreen();
            }
            
        }


        for (int i = 0; i < Tiles.Count; i++)
        {
            //checks to see if Tile(i) is equal to Current Tile for Active Unit
            if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile == Tiles[i])
            {
                Debug.Log("Keyboard Movement Starts");

                if (Key == "W")
                {
                    
                    //else
                   // {
                        if (Tiles[i - 12].GetComponent<TileInfo>().Occupied == false)
                        {
                            NxtTile = Tiles[i - 12].gameObject;
                            Key = null;
                            //i = Tiles.Count;
                            Debug.Log($"TILE W: {Tiles[i - 12]}");
                            //return;
                        }
                        else if (Tiles[i - 12].GetComponent<TileInfo>().Occupied == true)
                        {
                            NxtTile = Tiles[i - 12].gameObject;
                            
               

                            if (NxtTile.transform.GetChild(0).childCount > 0)
                            {
                                UnitInfo BattleTileObj = NxtTile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<UnitInfo>();
                                Debug.Log($"Ready Attack:   HP:  {BattleTileObj.UnitHealthCurr}");
                                if (BattleTileObj.UnitOwnership == UnitInfo.Owner.World)
                                {
                                    //Show EnemyUI Stats
                                    //BattleTileObj.ShowUnitDetails();
                                    //Deal Damage

                                    #region OldCode

                                    /*BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus = (Unit_BattlePanel.Status)UnityEngine.Random.Range(1, 3);

                                    if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Attacked)
                                    {
                                        if (BattleTileObj.UnitHealthCurr - 1 <= 0)
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Killed", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr = 0;
                                        }
                                        else
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Takes {1} DMG", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr -= 1;
                                        }
                                    }
                                    else if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Defending)
                                    {
                                        SetInteraction.Instance.ActivateInteractionText($"Enemy Defended", Color.blue, 2);
                                    }

                                    StartCoroutine(BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().ShowStatus(2));
                                }*/

                                    #endregion OldCode
                                    StartCoroutine(BattleTileObj.gameObject.transform.GetChild(0).GetComponent<Unit_BattlePanel>().ShowStatus(5));
                                    BattleScreen.SetActive(true);
                                    BattleSceneManager.Instance.SetVars();
                                    BattleTileObj.CanMove = false;
                                }
                                NxtTile = null;
                            }
                        }
                        else
                        {
                            Debug.Log("PATH BLOCKED - CANNOT MOVE TO TILE");
                        }

                    //}
                

                    break;
                    
                }

                if (Key == "S")
                {

                   // if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<TileInfo>().TransportTile == true)
                   // {
                    //    Debug.Log("Transport Tile");
                     //   PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<ConnectorTile>().ChangeScreen();
                   // }
                   // else
                   // {
                        if (Tiles[i + 12].GetComponent<TileInfo>().Occupied == false)
                        {
                            NxtTile = Tiles[i + 12].gameObject;
                            Key = null;
                            //break;
                        }
                        else
                        {
                            NxtTile = Tiles[i + 12].gameObject;

                            if (NxtTile.transform.GetChild(0).childCount > 0)
                            {
                                UnitInfo BattleTileObj = NxtTile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<UnitInfo>();
                                Debug.Log($"Ready Attack:   HP:  {BattleTileObj.UnitHealthCurr}");
                                if (BattleTileObj.UnitOwnership == UnitInfo.Owner.World)
                                {
                                    //Show EnemyUI Stats
                                    //BattleTileObj.ShowUnitDetails();
                                    //Deal Damage

                                    #region OldCode

                                    /*BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus = (Unit_BattlePanel.Status)UnityEngine.Random.Range(1, 3);

                                    if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Attacked)
                                    {
                                        if (BattleTileObj.UnitHealthCurr - 1 <= 0)
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Killed", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr = 0;
                                        }
                                        else
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Takes {1} DMG", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr -= 1;
                                        }
                                    }
                                    else if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Defending)
                                    {
                                        SetInteraction.Instance.ActivateInteractionText($"Enemy Defended", Color.blue, 2);
                                    }

                                    StartCoroutine(BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().ShowStatus(2));
                                }*/

                                    #endregion OldCode
                                    StartCoroutine(BattleTileObj.gameObject.transform.GetChild(0).GetComponent<Unit_BattlePanel>().ShowStatus(5));
                                    BattleScreen.SetActive(true);
                                    BattleSceneManager.Instance.SetVars();
                                    BattleTileObj.CanMove = false;
                                }
                                NxtTile = null;
                            }
                            else
                            {
                                Debug.Log("PATH BLOCKED - CANNOT MOVE TO TILE");
                            }
                            break;
                      //  }
                    }
                }

                if (Key == "D")
                {

                    //if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<TileInfo>().TransportTile == true)
                   // {
                   //     Debug.Log("Transport Tile");
                    //    PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<ConnectorTile>().ChangeScreen();
                   // }
                   // else
                   // {
                        if (Tiles[i + 1].GetComponent<TileInfo>().Occupied == false)
                        {
                            NxtTile = Tiles[i + 1].gameObject;
                            Key = null;
                            //break;
                        }
                        else
                        {
                            NxtTile = Tiles[i + 1].gameObject;

                            if (NxtTile.transform.GetChild(0).childCount > 0)
                            {
                                UnitInfo BattleTileObj = NxtTile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<UnitInfo>();
                                Debug.Log($"Ready Attack:   HP:  {BattleTileObj.UnitHealthCurr}");
                                if (BattleTileObj.UnitOwnership == UnitInfo.Owner.World)
                                {
                                    //Show EnemyUI Stats
                                    //BattleTileObj.ShowUnitDetails();
                                    //Deal Damage

                                    #region OldCode

                                    /*BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus = (Unit_BattlePanel.Status)UnityEngine.Random.Range(1, 3);

                                    if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Attacked)
                                    {
                                        if (BattleTileObj.UnitHealthCurr - 1 <= 0)
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Killed", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr = 0;
                                        }
                                        else
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Takes {1} DMG", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr -= 1;
                                        }
                                    }
                                    else if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Defending)
                                    {
                                        SetInteraction.Instance.ActivateInteractionText($"Enemy Defended", Color.blue, 2);
                                    }

                                    StartCoroutine(BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().ShowStatus(2));
                                }*/

                                    #endregion OldCode
                                    StartCoroutine(BattleTileObj.gameObject.transform.GetChild(0).GetComponent<Unit_BattlePanel>().ShowStatus(5));
                                    BattleScreen.SetActive(true);
                                    BattleSceneManager.Instance.SetVars();
                                    BattleTileObj.CanMove = false;
                                }
                                NxtTile = null;
                            }
                            else
                            {
                                Debug.Log("PATH BLOCKED - CANNOT MOVE TO TILE");
                            }
                            break;
                      //  }
                    }
                }

                if (Key == "A")
                {

                   // if (PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<TileInfo>().TransportTile == true)
                   // {
                   //     Debug.Log("Transport Tile");
                    //    PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile.GetComponent<ConnectorTile>().ChangeScreen();
                   // }
                   // else
                   // {
                        if (Tiles[i - 1].GetComponent<TileInfo>().Occupied == false)
                        {
                            NxtTile = Tiles[i - 1].gameObject;
                            Key = null;
                            //break;
                        }
                        else
                        {
                            NxtTile = Tiles[i - 1].gameObject;

                            if (NxtTile.transform.GetChild(0).childCount > 0)
                            {
                                UnitInfo BattleTileObj = NxtTile.transform.GetChild(0).GetChild(0).gameObject.GetComponent<UnitInfo>();

                                Debug.Log($"Ready Attack:   HP:  {BattleTileObj.UnitHealthCurr}");
                                if (BattleTileObj.UnitOwnership == UnitInfo.Owner.World)
                                {
                                    //Show EnemyUI Stats
                                    //BattleTileObj.ShowUnitDetails();
                                    //Deal Damage

                                    #region OldCode

                                    /*BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus = (Unit_BattlePanel.Status)UnityEngine.Random.Range(1, 3);

                                    if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Attacked)
                                    {
                                        if (BattleTileObj.UnitHealthCurr - 1 <= 0)
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Killed", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr = 0;
                                        }
                                        else
                                        {
                                            SetInteraction.Instance.ActivateInteractionText($"Enemy Takes {1} DMG", Color.red, 2);
                                            BattleTileObj.UnitHealthCurr -= 1;
                                        }
                                    }
                                    else if (BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().UnitStatus == Unit_BattlePanel.Status.Defending)
                                    {
                                        SetInteraction.Instance.ActivateInteractionText($"Enemy Defended", Color.blue, 2);
                                    }

                                    StartCoroutine(BattleTileObj.transform.GetChild(0).gameObject.GetComponent<Unit_BattlePanel>().ShowStatus(2));
                                }*/

                                    #endregion OldCode
                                    StartCoroutine(BattleTileObj.gameObject.transform.GetChild(0).GetComponent<Unit_BattlePanel>().ShowStatus(5));
                                    BattleScreen.SetActive(true);
                                    BattleSceneManager.Instance.SetVars();
                                    BattleTileObj.CanMove = false;
                                }
                            }
                            else
                            {
                                Debug.Log("PATH BLOCKED - CANNOT MOVE TO TILE");
                            }
                            break;
                        }
                    //}
                }
            }
            //SET NXT TILE USING WASD
        }
        if (NxtTile != null && NxtTile.GetComponent<TileInfo>().Occupied != true)
        {
            PlayerActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().OccupiedUnit = null;
            PlayerActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().Occupied = false;
            PlayerActiveUnit.transform.SetParent(NxtTile.transform.GetChild(0), false);
            PlayerActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().Occupied = true;
            PlayerActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<Button>().image.color = Color.white;
            PlayerActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().OccupiedUnit = PlayerActiveUnit;
            //NxtTile = null;
            Key = null;
        }
    }

    #endregion Movement

    #region ResetTiles

    public void ResetTiles()
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].GetComponent<Image>().color = Color.white;
            Tiles[i].GetComponent<TileInfo>().CanMoveTo = false;

            if (Tiles[i].transform.GetChild(0).transform.childCount > 0)
            {
                Tiles[i].GetComponent<TileInfo>().Occupied = true;
            }
            else
            {
                Tiles[i].GetComponent<TileInfo>().Occupied = false;
            }
        }
    }

    #endregion ResetTiles
}