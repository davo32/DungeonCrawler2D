using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour
{
    GameObject PlayerInterfaceObj;
    GameObject PlayerOneUI;

    public GameObject TileSelf;
    public GameObject TileChild;

    public GameObject OccupiedUnit;

    public bool P1SummonZone;
    public bool P2SummonZone;

    public bool EnemySpawnTile;

    //for tiles that lead to another room
    public bool TransportTile;

    public bool Occupied;
    public bool CanMoveTo;
    
    [Header("3D")]
    [SerializeField] private GameObject Obj;

    private CanvasGroup RoomObject;

    #region TextureTypes
    public Sprite None;
    public Sprite SummonZone;
    public Sprite Path;
    public Sprite Wall;
    public Sprite Stairs;
    public Sprite Door;
    public Sprite MStone;


    #endregion

    public enum TileType { None, Path, Wall, Stairs,SummonZone,Door,MStone };
    public TileType GroundType = TileType.Path;
    
    private void Start()
    {
        TileSelf = gameObject;
        TileChild = TileSelf.transform.GetChild(0).gameObject;
        PlayerInterfaceObj = GameObject.FindGameObjectWithTag("PlayerInterface");
        PlayerOneUI = GameObject.FindGameObjectWithTag("P1 UI");
        RoomObject = transform.parent.gameObject.GetComponent<CanvasGroup>();
        
        switch (GroundType)
        {
            case TileType.None:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = None;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = None;
                    Occupied = false;
                    break;
                }

            case TileType.Path:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Path;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = Path;
                    transform.GetChild(2).gameObject.SetActive(false);
                    Occupied = false;
                    break;
                }
            case TileType.Wall:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Wall;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = Wall;
                    transform.GetChild(2).gameObject.SetActive(false);
                    Occupied = true;
                    break;
                }
            case TileType.Stairs:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Stairs;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = Stairs;
                    transform.GetChild(2).gameObject.SetActive(false);
                    Occupied = false;
                    break;
                }
            case TileType.Door:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Door;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = Door;
                    Occupied = true;
                    break;
                }
            case TileType.SummonZone:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = SummonZone;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = SummonZone;
                    transform.GetChild(2).gameObject.SetActive(false);
                    Occupied = false;
                    break;
                }
            case TileType.MStone:
                {
                    TileSelf.transform.GetChild(0).GetComponentInChildren<Image>().sprite = SummonZone;
                    TileSelf.transform.GetComponentInChildren<Image>().sprite = MStone;
                    transform.GetChild(2).gameObject.SetActive(false);
                    Occupied = false;
                    break;
                }

        }
    }


    public void SetUnit()
    {
        //Debug.Log(GameManager.Instance.PlayerActiveUnit.name);
        //if(GroundType == TileType.SummonZone && GameManager.Instance.PlayerActiveUnit != null)
            //DeSpawn();
        
        if (PlayerInterfaceObj.GetComponent<PlayerInterface>().CanSummon && !Occupied)
        {
            if (P1SummonZone)
            {
                GameObject Unit = Instantiate(GameManager.Instance.UnitPrefab, TileChild.transform, false);

                Unit.GetComponent<UnitInfo>().CardInfo = PlayerInterfaceObj.GetComponent<PlayerInterface>().CardSelected;
                Unit.GetComponent<UnitInfo>().UnitOwnership = UnitInfo.Owner.PlayerOne;
                Unit.GetComponent<UnitInfo>().UType = UnitInfo.UnitType.Player;

                TileSelf.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
                OccupiedUnit = Unit;

                Occupied = true;
                PlayerInterfaceObj.GetComponent<PlayerInterface>().CanSummon = false;
                PlayerInterfaceObj.GetComponent<PlayerInterface>().SummonMenu.SetActive(false);

                PlayerInterfaceObj.GetComponent<PlayerInterface>().CardToHide.GetComponent<CanvasGroup>().alpha = 0.0f;
                PlayerInterfaceObj.GetComponent<PlayerInterface>().CardToHide.GetComponent<Button>().interactable = false;
                
                GameManager.Instance.PlayerActiveUnit = Unit;

                PlayerOneUI.GetComponent<PlayerInfo>().PlayerNameUI.text = Unit.GetComponent<UnitInfo>().CardInfo.CardName;

                SetInteraction.Instance.ActivateInteractionText($"{Unit.GetComponent<UnitInfo>().CardInfo.CardName} Summoned",Color.magenta,3);

            }
        }
    }

    public void MoveUnit()
    {
            
            //Check to see if unit can move and if tile is not occupied
            if (GameManager.Instance.AIActiveUnit.GetComponent<UnitInfo>().CanMove && !Occupied)
            {
                GameManager.Instance.AIActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().Occupied = false;

                //Moves Unit to new Tile
                GameManager.Instance.AIActiveUnit.transform.SetParent(TileSelf.transform.GetChild(0), false);

                //Sets new parent Occupied flag to true
                GameManager.Instance.AIActiveUnit.transform.parent.transform.parent.gameObject.GetComponent<TileInfo>().Occupied = true;

                //Reset Tile Indicators
                GameManager.Instance.ResetTiles();
            }
            else
            {

                //Reset Tile Indicators
                GameManager.Instance.ResetTiles();
            }
            
            //function here
    }

    void DeSpawn()
    {
        Destroy(GameManager.Instance.PlayerActiveUnit);
        PlayerInterfaceObj.GetComponent<PlayerInterface>().CardToHide.GetComponent<CanvasGroup>().alpha = 1.0f;
        Debug.Log("Despawned Character");
    }

    public void SetUnitParent(GameObject Obj)
    {
        Obj.transform.SetParent(TileChild.transform, false);
    }
    private void Update()
    {
        //3D///////////////////////////////////////////////////////////////////////////////////////////////////////
        Obj.SetActive(RoomObject.alpha > 0.0f && GroundType == TileType.Wall);
        transform.GetChild(2).gameObject.SetActive(transform.parent.GetComponent<CanvasGroup>().alpha > 0.0f && GroundType == TileType.Door);
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (GroundType == TileType.Wall)
        {
            Occupied = true;
        }

        ColorBlock colorvar = gameObject.GetComponent<Button>().colors;
        
        if (Occupied && OccupiedUnit)
        {
            if (OccupiedUnit.GetComponent<UnitInfo>().UType == UnitInfo.UnitType.Enemy)
            {
                colorvar.highlightedColor = Color.red;
                gameObject.GetComponent<Button>().colors = colorvar;

            }
            else if (OccupiedUnit.GetComponent<UnitInfo>().UType == UnitInfo.UnitType.Player)
            {
                colorvar.highlightedColor = Color.green;
                gameObject.GetComponent<Button>().colors = colorvar;
            }
        }
        else
        {
            colorvar.highlightedColor = Color.black;
            gameObject.GetComponent<Button>().colors = colorvar;
        }
    }

    
}
