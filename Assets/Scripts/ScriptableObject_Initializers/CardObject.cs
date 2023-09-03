using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/NewCard", order = 1)]
public class CardObject : ScriptableObject
{
    [Header("Card General Variables")]
    public string CardName;
    public Sprite CardIcon;

    [Header("Card Summoning Cost")]
    public int SummoningPoints;

    [Header("Card Stats")]
    public int AttackPoints;
    public int DefensePoints;
    public int HealthPoints;

}
