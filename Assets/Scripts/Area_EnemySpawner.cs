using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_EnemySpawner : MonoBehaviour
{
    public List<GameObject> SpawnTiles = new List<GameObject>();
    public GameObject UnitPrefab;
    public List<CardObject> SpawnUnits = new List<CardObject>();
    public int spawnedUnitsCount = 0;
    public int MaxEnemyUnitsInPlay = 0;
    public float RespawnTimer = 0;

    private void Start()
    {
        spawnedUnitsCount = SpawnUnits.Count;

        StartCoroutine(WaitNSpawn(RespawnTimer));
    }


    public IEnumerator WaitNSpawn(float respawnTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);

            if (spawnedUnitsCount <= MaxEnemyUnitsInPlay)
            {
                SetPrefabData(UnitPrefab);
                
                GameObject Tile = SpawnTiles[Random.Range(0, SpawnTiles.Count)];
                while(Tile.transform.childCount > 0)
                {
                    Tile = SpawnTiles[Random.Range(0, SpawnTiles.Count)];
                }
                GameObject GO = Instantiate(UnitPrefab, Tile.transform.parent);
                GO.transform.SetParent(Tile.transform,false);

                Tile.GetComponent<TileInfo>().Occupied = true;
                Tile.GetComponent<TileInfo>().OccupiedUnit = GO;
                spawnedUnitsCount++;
            }

           
        }
    }

    public void SetPrefabData(GameObject GO)
    {
        GO.GetComponent<UnitInfo>().CardInfo = SpawnUnits[Random.Range(0, SpawnUnits.Count)];
        GO.GetComponent<UnitInfo>().UnitOwnership = UnitInfo.Owner.World;
        GO.GetComponent<UnitInfo>().UType = UnitInfo.UnitType.Enemy;
        GO.GetComponent<UnitInfo>().CanMove = true;
    }
}
