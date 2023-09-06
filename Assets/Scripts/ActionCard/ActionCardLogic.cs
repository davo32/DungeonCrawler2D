using UnityEngine;

public class ActionCardLogic : ScriptableObject
{
    [Header("General")]
    public string actionName;
    public Sprite icon;
    public int statAmount;
    //used to see which card effect gets used first
    public int effectSpeed;
    public enum EType {None,Damage,Defend,Utility };
    public EType effectType = EType.None;

    public virtual int ApplyEffect(int stat)
    {
        return -1;
    }
}
