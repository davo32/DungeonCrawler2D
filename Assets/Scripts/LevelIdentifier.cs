using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIdentifier : MonoBehaviour
{
    public string LevelName;

    public List<GameObject> LevelTiles = new List<GameObject>();
    public List<GameObject> LevelTilesTemp;
    public void SetTiles()
    {
        LevelTilesTemp = new List<GameObject>(LevelTiles);

        GameManager.Instance.Tiles.Clear();
        GameManager.Instance.Tiles = LevelTilesTemp;
        Debug.Log($"{LevelName} : Tiles Set!!");
    }

    private void Update()
    {
        
    }
}
