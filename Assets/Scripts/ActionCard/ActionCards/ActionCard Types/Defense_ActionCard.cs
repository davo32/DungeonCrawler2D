using UnityEngine;

[CreateAssetMenu(fileName = "NewDefenseActionCard", menuName = "ScriptableObjects/DefenseActionCard", order = 1)]
public class Defense_ActionCard : ActionCardLogic
{
    private void Awake() => effectType = EType.Defend;

    public override int ApplyEffect(int enemyDamage)
    {
        //if defense value is less than enemyDamage subtract defenseVal from Damage
        if (enemyDamage > statAmount)
        {
            return (enemyDamage -= statAmount);
        }
        //if defense value is more than or equal to enemyDamage then we nullify the damage
        else 
        {
            return 0;
        }
    }
}
