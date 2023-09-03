using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ActionCard", menuName = "ScriptableObjects/NewActionCard", order = 1)]
public class ActionCardLogic : ScriptableObject
{
    public string ActionName;
    public Sprite Icon;
    public int StatAmount;
    //used to see which card effect gets used first
    public int EffectSpeed;
    public enum EType {None,Damage,Defend,Utility };
    public EType EffectType = EType.None;


}
