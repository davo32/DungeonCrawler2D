using UnityEngine;

[CreateAssetMenu(fileName = "NewHealActionCard", menuName = "ScriptableObjects/HealActionCard", order = 1)]
public class Heal_ActionCard : ActionCardLogic
{
    private void Awake() => effectType = EType.Utility;

    public override int ApplyEffect(int health)
    {
       return health += statAmount;
    }
}
