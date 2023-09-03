using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/NewLevel", order = 1)]
public class LevelInfo : ScriptableObject
{
    public string LevelName;

    public GameObject LevelGrid;

    //Holds the Tiles that will spawn enemies that the AI will control numbered from for loop
    public List<GameObject> SpawnLocations = new List<GameObject>();

    //Level Name
    //Spawn Locations (List)
    //Units to Spawn (List)
    //
}
