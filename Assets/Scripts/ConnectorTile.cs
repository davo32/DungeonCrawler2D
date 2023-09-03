using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectorTile : MonoBehaviour
{
    public CanvasGroup CurrScreen;
    public CanvasGroup nextScreen;

    public GameObject NextTile;
    GameObject NextChild;

    public KeyCode Direction;

    // Start is called before the first frame update
    void Start()
    {
        NextChild = NextTile.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    public void ChangeScreen()
    {
       // if (GetComponent<TileInfo>().Occupied)
        //{
            //if (Input.GetKeyDown(Direction))
            //{
               

                Hide(CurrScreen);
                Show(nextScreen);
                nextScreen.gameObject.GetComponent<LevelIdentifier>().SetTiles();

               // GameManager.Instance.NxtTile = NextTile;

               GetComponent<TileInfo>().Occupied = false;

               GameManager.Instance.PlayerActiveUnit.transform.SetParent(NextChild.transform, false);
               GameManager.Instance.PlayerActiveUnit.GetComponent<UnitInfo>().CurrTile = NextTile;
               NextTile.GetComponent<TileInfo>().Occupied = true;
               GetComponent<Button>().image.color = Color.white;

                GameManager.Instance.LevelUI.GetComponent<PlayerInfo>().PlayerNameUI.text = nextScreen.gameObject.GetComponent<LevelIdentifier>().LevelName;
            //}
        //}
    }


    public void Hide(CanvasGroup Obj)
    {
        Obj.alpha = 0.0f;
        Obj.interactable = false;
        Obj.blocksRaycasts = false;
    }

    public void Show(CanvasGroup Obj)
    {
        Obj.alpha = 1.0f;
        Obj.interactable = true;
        Obj.blocksRaycasts = true;
    }
}
