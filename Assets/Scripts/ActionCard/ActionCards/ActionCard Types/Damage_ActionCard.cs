using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageActionCard", menuName = "ScriptableObjects/DamageActionCard", order = 1)]
public class Damage_ActionCard : ActionCardLogic
{
    private void Awake() => effectType = EType.Damage;

    public override int ApplyEffect(int health)
    {
       return (health -= statAmount);
    }
}
