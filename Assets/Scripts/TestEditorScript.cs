using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TestEditorScript : MonoBehaviour
{
    public GameObject[] AllTiles;

    private void Update()
    {
        SetTileInfo();
    }

    public void SetTileInfo()
    {
        AllTiles = GameObject.FindGameObjectsWithTag("Tile");
        //foreach tile check enum of tile type
        //get child of tile and set the image to its correct image

        for (int i = 0; i < AllTiles.Length; i++)
        {
            if (AllTiles[i].GetComponent<TileInfo>().GroundType == TileInfo.TileType.Path)
            {
                AllTiles[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Path;
                AllTiles[i].transform.gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Path;
                AllTiles[i].GetComponent<TileInfo>().Occupied = false;
            }
            else if (AllTiles[i].GetComponent<TileInfo>().GroundType == TileInfo.TileType.Wall)
            {
                AllTiles[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Wall;
                AllTiles[i].transform.gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Wall;
                AllTiles[i].GetComponent<TileInfo>().Occupied = true;
            }
            else if (AllTiles[i].GetComponent<TileInfo>().GroundType == TileInfo.TileType.Door)
            {
                AllTiles[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Door;
                AllTiles[i].transform.gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().Door;
                AllTiles[i].GetComponent<TileInfo>().Occupied = true;
            }
            else if (AllTiles[i].GetComponent<TileInfo>().GroundType == TileInfo.TileType.SummonZone)
            {
                AllTiles[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().SummonZone;
                AllTiles[i].transform.gameObject.GetComponent<Image>().sprite = AllTiles[i].GetComponent<TileInfo>().SummonZone;
                AllTiles[i].GetComponent<TileInfo>().Occupied = false;
            }
        }
    }
}
