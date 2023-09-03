using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericItem : ScriptableObject
{
    //container of information for generic items
    public string ItemName;
    public Sprite ItemIcon;

    public enum ItemType {Generic,Weapon,Armor,Consumable,KeyItem};
    public ItemType IType = ItemType.Generic;
}
